using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToDoApp.Application.Services;
using ToDoApp.Infrastructure;

namespace ToDoApp.UI
{
    /// <summary>
    /// Interaction logic for AnalyticsPage.xaml
    /// </summary>

    public partial class AnalyticsPage : Page
    {
        private readonly ToDoAppContext _context;
        private readonly ToDoService _toDoService;
        public AnalyticsPage()
        {
            InitializeComponent();
            _context = new ToDoAppContext();
            var repository = new ToDoRepository(_context);
            _toDoService = new ToDoService(repository);
        }

        private async void ShowAnalyticsButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                var startDate = StartDatePicker.SelectedDate ?? DateTime.MinValue;
                var endDate = EndDatePicker.SelectedDate ?? DateTime.MaxValue;
                // Compter le nombre de tâches créées dans la plage de dates spécifiée
                var createdCount = _context.ToDos.Count(t => t.CreatedAt >= startDate && t.CreatedAt <= endDate);
                // Compter le nombre de tâches complètes dans la plage de dates spécifiée
                var completedCount = _context.ToDos.Count(t => t.CompletedAt >= startDate && t.CompletedAt <= endDate && t.Status);

                ResultTextBlock.Text = $"Created: {createdCount}, Completed: {completedCount}";

                // Get the most productive day
                var mostProductiveDay = await _toDoService.GetMostProductiveDayAsync();
                MostProductiveDayTextBlock.Text = $"Most Productive Day: {mostProductiveDay}";
            }
            catch (Exception ex)
            {
                ResultTextBlock.Text = $"Error: {ex.Message}";
            }


           
        }
       

        private void BackToMainPageButton_Click(object sender, RoutedEventArgs e)
        {
            // Retourner à la page principale
            //NavigationService.Navigate(new MainWindow());
            // pour fermer l'ancienne fenêtre et ouvrir une nouvelle 
            var mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this)?.Close();
        }
    }
}
    

