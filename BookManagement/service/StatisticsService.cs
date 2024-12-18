using BookManagement.entity;
using BookManagement.mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookManagement.service
{
    public class StatisticsService
    {
        public enum StatisticsType
        {
            All,
            Yearly,
            Monthly,
            Daily
        }

        public class StatisticsResult
        {
            public DateTime Date { get; set; }
            public string OrderType { get; set; }
            public int OrderId { get; set; }
            public string ISBN { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
            public decimal TotalAmount { get; set; }
        }

        public class StatisticsSummary
        {
            public List<StatisticsResult> Details { get; set; }
            public decimal TotalCost { get; set; }
            public decimal TotalRevenue { get; set; }
            public decimal Profit { get; set; }
        }

        private List<PurchaseOrder> _purchaseOrders;
        private List<SalesOrder> _salesOrders;
        private List<PurchaseOrderDetail> _purchaseOrderDetails;
        private List<SalesOrderDetail> _salesOrderDetails;

        public StatisticsService()
        {
            LoadData();
        }

        private async Task LoadData()
        {
            await Task.Run(() =>
            {
                _purchaseOrders = PurchaseOrderMapper.GetPurchaseOrders();
                _salesOrders = SalesOrderMapper.getAll();
                _purchaseOrderDetails = PurchaseOrderDetailMapper.GetPurchaseOrderDetails();
                _salesOrderDetails = _salesOrders.SelectMany(so => SalesOrderDetailMapper.getAllBySalesOrderId(so.SalesOrderId)).ToList();
            });
        }

        public async Task<StatisticsSummary> GetStatisticsAsync(int? year = null, int? month = null, int? day = null)
        {
            await LoadData();  // Ensure data is loaded

            var statisticsType = DetermineStatisticsType(year, month, day);
            var results = new List<StatisticsResult>();

            switch (statisticsType)
            {
                case StatisticsType.All:
                    results = await Task.Run(() => GetAllStatistics());
                    break;
                case StatisticsType.Yearly:
                    results = await Task.Run(() => GetYearlyStatistics(year.Value));
                    break;
                case StatisticsType.Monthly:
                    results = await Task.Run(() => GetMonthlyStatistics(year.Value, month.Value));
                    break;
                case StatisticsType.Daily:
                    results = await Task.Run(() => GetDailyStatistics(year.Value, month.Value, day.Value));
                    break;
            }

            var summary = new StatisticsSummary
            {
                Details = results,
                TotalCost = results.Where(r => r.OrderType == "采购").Sum(r => r.TotalAmount),
                TotalRevenue = results.Where(r => r.OrderType == "销售").Sum(r => r.TotalAmount)
            };
            summary.Profit = summary.TotalRevenue - summary.TotalCost;
            return summary;
        }

        private StatisticsType DetermineStatisticsType(int? year, int? month, int? day)
        {
            if (!year.HasValue)
                return StatisticsType.All;
            if (day.HasValue && month.HasValue)
                return StatisticsType.Daily;
            if (month.HasValue)
                return StatisticsType.Monthly;
            return StatisticsType.Yearly;
        }

        private List<StatisticsResult> GetAllStatistics()
        {
            var purchaseResults = _purchaseOrders
                .Join(_purchaseOrderDetails,
                    po => po.PurchaseOrderId,
                    pod => pod.PurchaseOrderId,
                    (po, pod) => new StatisticsResult
                    {
                        Date = po.OrderDate,
                        OrderType = "采购",
                        OrderId = po.PurchaseOrderId,
                        ISBN = pod.Isbn,
                        Price = pod.Price,
                        Quantity = pod.Amount,
                        TotalAmount = pod.Price * pod.Amount
                    });

            var salesResults = _salesOrders
                .Join(_salesOrderDetails,
                    so => so.SalesOrderId,
                    sod => sod.SalesOrderId,
                    (so, sod) => new StatisticsResult
                    {
                        Date = so.OrderDate,
                        OrderType = "销售",
                        OrderId = so.SalesOrderId,
                        ISBN = sod.isbn,
                        Price = sod.Price,
                        Quantity = sod.Amount,
                        TotalAmount = sod.Price * sod.Amount
                    });

            return purchaseResults.Concat(salesResults).OrderBy(r => r.Date).ToList();
        }

        private List<StatisticsResult> GetYearlyStatistics(int year)
        {
            var yearStart = new DateTime(year, 1, 1);
            var yearEnd = yearStart.AddYears(1);

            return GetAllStatistics().Where(r => r.Date >= yearStart && r.Date < yearEnd).ToList();
        }

        private List<StatisticsResult> GetMonthlyStatistics(int year, int month)
        {
            var monthStart = new DateTime(year, month, 1);
            var monthEnd = monthStart.AddMonths(1);

            return GetAllStatistics().Where(r => r.Date >= monthStart && r.Date < monthEnd).ToList();
        }

        private List<StatisticsResult> GetDailyStatistics(int year, int month, int day)
        {
            var targetDate = new DateTime(year, month, day);

            return GetAllStatistics().Where(r => r.Date.Date == targetDate.Date).ToList();
        }
    }
}