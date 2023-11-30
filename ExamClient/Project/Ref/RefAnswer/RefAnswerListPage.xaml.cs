using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamModels;
using System.Collections.ObjectModel;
using System.Globalization;
using ExamClient.Resources.Resx;

namespace Client.Project
{

    public partial class RefAnswerListPage : ContentPage
    {
        public CommandCL command = new CommandCL();
        private AnswerEditorViewModel viewModel;
        private AnswerManager viewModelManager;
        public ExamModels.Answer vSelectedItem { get; set; }
        public int Mode { get; set; }

        public RefAnswerListPage()
        {
            try
            {


                InitializeComponent();
                viewModel = new AnswerEditorViewModel();
                viewModelManager = new AnswerManager();
                AnswerList1.ItemsSource = GetAnswers();
            }
            catch (Exception ex)
            {
                DisplayAlert(AppResources.Ошибка, ex.Message, AppResources.Ок);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateForm();
        }

        private void UpdateForm()
        {
            AnswerList1.ItemsSource = GetAnswers();
        }

        private void ContentPage_Loaded(object sender, EventArgs e)
        {
            // Your code here
            UpdateForm();
        }

        private List<Answer> GetAnswers()
        {
            List<Answer> answerList = new List<Answer>();

            CommandCL.AnswerListGet = null;
            viewModelManager.GetAnswerList();

            if (CommandCL.AnswerListGet == null)
            {
                // Handle the case when the questions list is null
            }
            else
            {
                for (int i = 0; i < CommandCL.AnswerListGet.ListAnswer.Count; i++)
                {
                    var answer = new Answer { Answers = CommandCL.AnswerListGet.ListAnswer[i], EditCommand = new Command(Edit), DelCommand = new Command(Del) };
                    answerList.Add(answer);
                }
            }

            return answerList;
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var selectedAnswer = (Answer)e.SelectedItem;
            //await DisplayAlert("Выбранный вопрос", selectedAnswer.Answers.AnswerOptions, "OK");
            ((ListView)sender).SelectedItem = null;

            vSelectedItem = selectedAnswer.Answers;

            if (Mode == 1)
            {
                // Закрытие формы RefExamsListPage
                await Navigation.PopModalAsync();
            }
        }

        private void Edit(object answer)
        {
            var selectedAnswer = (Answer)answer;
            Navigation.PushAsync(new AnswerEditor(selectedAnswer.Answers));
        }

        private void Del(object answer)
        {
            var selectedAnswer = (Answer)answer;

            viewModelManager.DeleteAnswerData(selectedAnswer.Answers);

            DisplayAlert(AppResources.Удаляетсявопрос , selectedAnswer.Answers.AnswerOptions, AppResources.Ок);
            UpdateForm();
        }

        private async void GoBack(object sender, EventArgs e)
        {
            //var mainPage = new Client.Main.Admin();
            //var navigationPage = new NavigationPage(mainPage);

            //Application.Current.MainPage = navigationPage;

            await Navigation.PopAsync();

        }

        public class Answer
        {
            public ExamModels.Answer Answers { get; set; }
            public Command EditCommand { get; set; }
            public Command DelCommand { get; set; }
        }

        private void CreateButtonClicked(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new AnswerCreate());
            }
            catch(Exception ex) 
            {
                DisplayAlert(AppResources.Ошибкапереходанавопроссоздать, ex.Message,AppResources.Ок);
            }
        }
    }
}