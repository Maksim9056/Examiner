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
    public partial class DocTestQuestionListPage : ContentPage
    {
        public CommandCL command = new CommandCL();
        private TestQuestionEditorViewModel viewModel;
        private TestQuestionManager viewModelManager;
        private ExamModels.Test CurrrentTest;

        public DocTestQuestionListPage(ExamModels.Test test)
        {
            InitializeComponent();
            viewModel = new TestQuestionEditorViewModel();
            viewModelManager = new TestQuestionManager();
            CurrrentTest = test;
            QuestionList1.ItemsSource = GetTestQuestions(test);
            Title =AppResources.��������������� + test.Name_Test;
#pragma warning disable CS0618 // ��� ��� ���� �������
            MessagingCenter.Subscribe<DocTestQuestionListPage>(this, "UpdateForm", (sender) =>
            {
                // Perform the necessary updates to the form here
                // For example, update the fields, refresh data, etc.
                QuestionList1.ItemsSource = GetTestQuestions(test);
            });
#pragma warning restore CS0618 // ��� ��� ���� �������
        }

        private void UpdateForm(ExamModels.Test test)
        {
            QuestionList1.ItemsSource = GetTestQuestions(test);
        }

        private void ContentPage_Loaded(object sender, EventArgs e)
        {
            // Your code here
        }

        private List<RefTestQuestion> GetTestQuestions(ExamModels.Test test)
        {
            List<RefTestQuestion> testQuestionList = new List<RefTestQuestion>();

            CommandCL.ExamsListGet = null;
            viewModelManager.GetTestQuestionList(test);

            if (CommandCL.TestQuestionListGet == null)
            {
                // Handle the case when the test list is null
            }
            else
            {
                for (int i = 0; i < CommandCL.TestQuestionListGet.ListTestQuestion.Count; i++)
                {
                    var refTestQuestion = new RefTestQuestion { TestQuestion = CommandCL.TestQuestionListGet.ListTestQuestion[i], EditCommand = new Command(EditTestQuestion), DelCommand = new Command(DelTestQuestion) };
                    testQuestionList.Add(refTestQuestion);
                }
            }

            return testQuestionList;
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var selectedTestQuestion = (RefTestQuestion)e.SelectedItem;
            await DisplayAlert(AppResources.���������������, selectedTestQuestion.TestQuestion.IdQuestions.QuestionName, AppResources.��);
            ((ListView)sender).SelectedItem = null;
        }

        private void EditTestQuestion(object testQuestion)
        {
            var selectedTestQuestion = (RefTestQuestion)testQuestion;
            Navigation.PushAsync(new QuestionsEditor(selectedTestQuestion.TestQuestion.IdQuestions));
        }

        private void DelTestQuestion(object testQuestion)
        {
            var selectedTestQuestion = (RefTestQuestion)testQuestion;

            viewModelManager.DeleteTestQuestionData(selectedTestQuestion.TestQuestion);

            DisplayAlert(AppResources.���������������, selectedTestQuestion.TestQuestion.IdQuestions.QuestionName, AppResources.��);
            UpdateForm(CurrrentTest);
        }

        private async void GoBack(object sender, EventArgs e)
        {
            //if (Application.Current.MainPage is NavigationPage navigationPage)
            //{
            //    navigationPage.Navigation.PopAsync();
            //}
            //await Shell.Current.Navigation.PopAsync();
            await Navigation.PopAsync();


        }

        public class RefTestQuestion
        {
            public ExamModels.TestQuestion TestQuestion { get; set; }
            public Command EditCommand { get; set; }
            public Command DelCommand { get; set; }
        }

        private async void CreateButtonClicked(object sender, EventArgs e)
        {
            var refQuestionsListPage = new RefQuestionsListPage();
            refQuestionsListPage.Mode = 1;
            refQuestionsListPage.Disappearing += (s, args) =>
            {
                if (refQuestionsListPage.vSelectedItem != null)
                {
                    var selectedItem = refQuestionsListPage.vSelectedItem;

                    TestQuestion aTestQ = new TestQuestion();
                    aTestQ.IdQuestions = selectedItem;
                    aTestQ.IdTest = CurrrentTest;
                    viewModelManager.CreateTestQuestionData(aTestQ);

                    // Clear the selected item in RefQuestionsListPage
                    refQuestionsListPage.vSelectedItem = null;

                    // Send a message to update the current form
#pragma warning disable CS0618 // ��� ��� ���� �������
                    MessagingCenter.Send(this, "UpdateForm");
#pragma warning restore CS0618 // ��� ��� ���� �������
                }
            };

            await Navigation.PushModalAsync(refQuestionsListPage);
        }



        private void ContentPageLoaded(object sender, EventArgs e)
        {

        }
    }
}