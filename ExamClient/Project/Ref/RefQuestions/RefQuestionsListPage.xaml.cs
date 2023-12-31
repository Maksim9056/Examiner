using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamModels;
using System.Collections.ObjectModel;
using ExamClient.Resources.Resx;

namespace Client.Project
{
    public partial class RefQuestionsListPage : ContentPage
    {
        public CommandCL command = new CommandCL();
        private QuestionsEditorViewModel viewModel;
        private QuestionManager viewModelManager;
        public ExamModels.Questions vSelectedItem { get; set; }
        public int Mode { get; set; }


        public RefQuestionsListPage()
        {
            InitializeComponent();
            viewModel = new QuestionsEditorViewModel();
            viewModelManager = new QuestionManager();
            QuestionsList1.ItemsSource = GetQuestions();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateForm();
        }

        private void UpdateForm()
        {
            QuestionsList1.ItemsSource = GetQuestions();
        }

        private void ContentPage_Loaded(object sender, EventArgs e)
        {
            // Your code here
            UpdateForm();
        }

        private List<Questions> GetQuestions()
        {
            List<Questions> questionsList = new List<Questions>();

            CommandCL.QuestionsListGet = null;
            viewModelManager.GetQuestionList();

            if (CommandCL.QuestionsListGet == null)
            {
                // Handle the case when the questions list is null
            }
            else
            {
                for (int i = 0; i < CommandCL.QuestionsListGet.QuestionList.Count; i++)
                {
                    var question = new Questions { Question = CommandCL.QuestionsListGet.QuestionList[i], EditCommand = new Command(EditQuestion), DelCommand = new Command(DelQuestion) };
                    questionsList.Add(question);
                }
            }

            return questionsList;
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var selectedQuestion = (Questions)e.SelectedItem;
            //await DisplayAlert("��������� ������", selectedQuestion.Question.QuestionName, "OK");
            ((ListView)sender).SelectedItem = null;

            vSelectedItem = selectedQuestion.Question;

            if (Mode == 1) { await Navigation.PopModalAsync(); }

            await Navigation.PushAsync(new DocQuestionAnswerListPage(selectedQuestion.Question));
        }


        private void EditQuestion(object question)
        {
            var selectedQuestion = (Questions)question;
            Navigation.PushAsync(new QuestionsEditor(selectedQuestion.Question));
        }

        private void DelQuestion(object question)
        {
            var selectedQuestion = (Questions)question;

            viewModelManager.DeleteQuestionData(selectedQuestion.Question);

            DisplayAlert(AppResources.���������������, selectedQuestion.Question.QuestionName, AppResources.��);
            UpdateForm();
        }

        private async void GoBack(object sender, EventArgs e)
        {
            //var mainPage = new Client.Main.Admin();
            //var navigationPage = new NavigationPage(mainPage);

            //Application.Current.MainPage = navigationPage;
            await Navigation.PopAsync();

        }
        //private void GoBack(object sender, EventArgs e)
        //{
        //    if (Application.Current.MainPage is NavigationPage navigationPage)
        //    {
        //        navigationPage.Navigation.PopAsync();
        //    }
        //}

        public class Questions
        {
            public ExamModels.Questions Question { get; set; }
            public Command EditCommand { get; set; }
            public Command DelCommand { get; set; }
        }

        private  void CreateButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new QuestionsCreate());
        }


    }
}