using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
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
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Interfaces;
using ToDoApp.Infrastructure;

namespace ToDoApp.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
        public partial class MainWindow : Window
        {
        private readonly ToDoService _toDoService;
        public MainWindow()
            {
            InitializeComponent();
            var context = new ToDoAppContext();
            var repository = new ToDoRepository(context);
            _toDoService = new ToDoService(repository);
            LoadToDos();
        }
        private async void LoadToDos()
        {
            var todos = await _toDoService.GetAllToDosAsync();
            ToDoListBox.ItemsSource = todos;
        }
        private async void AddTaskButton_Click(object sender, RoutedEventArgs e)
            {
            var taskName = NewTaskTextBox.Text;
            if (!string.IsNullOrWhiteSpace(taskName))
            {
                await _toDoService.AddToDoAsync(taskName);
                LoadToDos();
            }

            NewTaskTextBox.Clear();
             }
        private async void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is ToDo toDo)
            {
                //toDo.MarkAsComplete();
                await _toDoService.MarkToDoAsCompleteAsync(toDo.Id);  // Update the database to save the changes
                LoadToDos(); // Refresh the list to reflect the change
            }
        }

        private async void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is ToDo toDo)
            {
                toDo.Status = false;
                toDo.CompletedAt = null;
                await _toDoService.MarkToDoAsCompleteAsync(toDo.Id);
                LoadToDos();
            }
        }
        private async void CompleteTask_Click(object sender, RoutedEventArgs e)
    {
        if (ToDoListBox.SelectedItem is ToDo selectedToDo)
        {
            await _toDoService.MarkToDoAsCompleteAsync(selectedToDo.Id);
            LoadToDos();
        }
    }
        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Récupérer l'ID de la tâche à supprimer à partir des paramètres de commande
            if (sender is Button button && button.CommandParameter is int taskId)
            {
                // Rechercher la tâche dans le contexte de la base de données
                //ToDo taskToDelete = await _toDoService.FindAsync(taskId);

                // if (taskToDelete != null)
                //  {
                // Supprimer la tâche
                // _context.ToDos.Remove(taskToDelete);
                // await _context.SaveChangesAsync();

                // appel de service
                await _toDoService.DeleteToDoAsync(taskId);

                    // Rafraîchir la liste après la suppression
                    LoadToDos();
                //}
            }
        }
        private void NewTaskTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            // Effacer le texte si c'est un texte d'indication
            if (NewTaskTextBox.Text == "Enter a new task...")
            {
                NewTaskTextBox.Clear();
            }
        }

        private void NewTaskTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            // Réinitialiser le texte d'indication si le champ est vide
            if (string.IsNullOrWhiteSpace(NewTaskTextBox.Text))
            {
                NewTaskTextBox.Text = "Enter a new task...";
            }
        }

        private void ViewAnalyticsButton_Click(object sender, RoutedEventArgs e)
        {
            // Ouvrir la page d'analytique
            AnalyticsPage analyticsPage = new AnalyticsPage();
            this.Content = analyticsPage; // Afficher la page d'analytique dans le contenu de la fenêtre principale
        }
    }


}

