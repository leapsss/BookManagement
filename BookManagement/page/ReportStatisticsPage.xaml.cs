using BookManagement.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Threading.Tasks;

namespace BookManagement.page
{
    public partial class ReportStatisticsPage : Page
    {
        private readonly StatisticsService _statisticsService;

        public ReportStatisticsPage()
        {
            InitializeComponent();
            _statisticsService = new StatisticsService();

            InitializeComboBoxes();
        }

        private void InitializeComboBoxes()
        {
            // Populate Year ComboBox
            YearComboBox.Items.Add(new ComboBoxItem { Content = "全部", Tag = (int?)null });
            int currentYear = DateTime.Now.Year;
            for (int year = currentYear; year >= currentYear - 10; year--)
            {
                YearComboBox.Items.Add(new ComboBoxItem { Content = year.ToString(), Tag = year });
            }

            // Populate Month ComboBox
            MonthComboBox.Items.Add(new ComboBoxItem { Content = "全部", Tag = (int?)null });
            for (int month = 1; month <= 12; month++)
            {
                MonthComboBox.Items.Add(new ComboBoxItem { Content = $"{month}月", Tag = month });
            }

            // Populate Day ComboBox
            UpdateDayComboBox();

            // Set default selections
            YearComboBox.SelectedIndex = 0;
            MonthComboBox.SelectedIndex = 0;
            DayComboBox.SelectedIndex = 0;

            // Add event handlers
            YearComboBox.SelectionChanged += (s, e) => UpdateComboBoxStates();
            MonthComboBox.SelectionChanged += (s, e) => UpdateComboBoxStates();
        }

        private void UpdateComboBoxStates()
        {
            int? selectedYear = (YearComboBox.SelectedItem as ComboBoxItem)?.Tag as int?;

            if (selectedYear.HasValue)
            {
                MonthComboBox.IsEnabled = true;
                int? selectedMonth = (MonthComboBox.SelectedItem as ComboBoxItem)?.Tag as int?;

                if (selectedMonth.HasValue)
                {
                    UpdateDayComboBox();
                    DayComboBox.IsEnabled = true;
                }
                else
                {
                    DayComboBox.SelectedIndex = 0;
                    DayComboBox.IsEnabled = false;
                }
            }
            else
            {
                MonthComboBox.SelectedIndex = 0;
                MonthComboBox.IsEnabled = false;
                DayComboBox.SelectedIndex = 0;
                DayComboBox.IsEnabled = false;
            }
        }

        private void UpdateDayComboBox()
        {
            DayComboBox.Items.Clear();
            DayComboBox.Items.Add(new ComboBoxItem { Content = "全部", Tag = (int?)null });

            int? year = (YearComboBox.SelectedItem as ComboBoxItem)?.Tag as int?;
            int? month = (MonthComboBox.SelectedItem as ComboBoxItem)?.Tag as int?;

            if (year.HasValue && month.HasValue)
            {
                int daysInMonth = DateTime.DaysInMonth(year.Value, month.Value);

                for (int day = 1; day <= daysInMonth; day++)
                {
                    DayComboBox.Items.Add(new ComboBoxItem { Content = $"{day}日", Tag = day });
                }
            }

            DayComboBox.SelectedIndex = 0;
        }

        private async void QueryButton_Click(object sender, RoutedEventArgs e)
        {
            await UpdateResults();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            YearComboBox.SelectedIndex = 0;
            MonthComboBox.SelectedIndex = 0;
            DayComboBox.SelectedIndex = 0;
            ResultsDataGrid.ItemsSource = null;
            TotalCostTextBlock.Text = "";
            TotalRevenueTextBlock.Text = "";
            ProfitTextBlock.Text = "";
            UpdateComboBoxStates();
        }

        private async Task UpdateResults()
        {
            int? year = (YearComboBox.SelectedItem as ComboBoxItem)?.Tag as int?;
            int? month = (MonthComboBox.SelectedItem as ComboBoxItem)?.Tag as int?;
            int? day = (DayComboBox.SelectedItem as ComboBoxItem)?.Tag as int?;

            try
            {
                StatisticsService.StatisticsSummary summary = await _statisticsService.GetStatisticsAsync(year, month, day);

                if (summary.Details.Count == 0)
                {
                    MessageBox.Show("没有找到符合条件的结果", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    ResultsDataGrid.ItemsSource = null;
                    TotalCostTextBlock.Text = "";
                    TotalRevenueTextBlock.Text = "";
                    ProfitTextBlock.Text = "";
                }
                else
                {
                    ResultsDataGrid.ItemsSource = summary.Details;
                    TotalCostTextBlock.Text = summary.TotalCost.ToString("C");
                    TotalRevenueTextBlock.Text = summary.TotalRevenue.ToString("C");
                    ProfitTextBlock.Text = summary.Profit >= 0 ? summary.Profit.ToString("C") : $"-{(-summary.Profit).ToString("C")}";
                    ProfitTextBlock.Foreground = summary.Profit >= 0 ? Brushes.Green : Brushes.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询过程中发生错误：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}