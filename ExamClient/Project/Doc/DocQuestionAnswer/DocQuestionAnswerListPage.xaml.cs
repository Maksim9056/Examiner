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
    public partial class DocQuestionAnswerListPage : ContentPage
    {
        public CommandCL command = new CommandCL();
        private QuestionAnswerEditorViewModel viewModel;
        private QuestionAnswerManager viewModelManager;
        private ExamModels.Questions CurrrentQuestions;

        public DocQuestionAnswerListPage(ExamModels.Questions questions)
        {
            InitializeComponent();
            viewModel = new QuestionAnswerEditorViewModel();
            viewModelManager = new QuestionAnswerManager();
            CurrrentQuestions = questions;
            QuestionList.ItemsSource = GetQuestionAnswer(questions);
            Title = AppResources.���������������� + questions.QuestionName;
#pragma warning disable CS0618 // ��� ��� ���� �������
            MessagingCenter.Subscribe<DocQuestionAnswerListPage>(this, "UpdateForm", (sender) =>
            {
                // Perform the necessary updates to the form here 
                // For example, update the fields, refresh data, etc. 
                QuestionList.ItemsSource = GetQuestionAnswer(questions);
            });
#pragma warning restore CS0618 // ��� ��� ���� �������
        }

        //void OnUpdateForm(DocQuestionAnswerListPage sender)
        //{
        //    // ��������� ����������� ���������� ����� �����
        //    // ��������, �������� ����, �������� ������ � �. �.
        //    QuestionList.ItemsSource = GetQuestionAnswer(CurrrentTest);
        //}

        //// �������� �� �������
        //DocQuestionAnswerListPage.UpdateForm += OnUpdateForm;

        //// ������� �� ������� (�� �������� ����������, ����� ��� ������ �� �����)
        //DocQuestionAnswerListPage.UpdateForm -= OnUpdateForm;

        private void UpdateForm(ExamModels.Questions test)
        {
            QuestionList.ItemsSource = GetQuestionAnswer(test);
        }

        private void ContentPage_Loaded(object sender, EventArgs e)
        {

            // Your code here 
        }

        private List<RefQuestionAnswer> GetQuestionAnswer(ExamModels.Questions questions)
        {
            List<RefQuestionAnswer> testQuestionList = new List<RefQuestionAnswer>();

            CommandCL.QuestionAnswerListGet = null;
            viewModelManager.GetQuestionAnswerList(questions);

            if (CommandCL.QuestionAnswerListGet == null)
            {
                // Handle the case when the test list is null 
            }
            else
            {
                for (int i = 0; i < CommandCL.QuestionAnswerListGet.ListQuestionAnswer.Count; i++)
                {
                    var refQuestionAnswer = new RefQuestionAnswer { QuestionAnswer = CommandCL.QuestionAnswerListGet.ListQuestionAnswer[i], EditCommand = new Command(Edit), DelCommand = new Command(Del) };
                    testQuestionList.Add(refQuestionAnswer);
                }
            }

            return testQuestionList;
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var selectedTestQuestion = (RefQuestionAnswer)e.SelectedItem;
            await DisplayAlert( AppResources.��������������, selectedTestQuestion.QuestionAnswer.Answer.AnswerOptions, AppResources.��);
            ((ListView)sender).SelectedItem = null;
        }

        private void Edit(object testQuestion)
        {
            var selectedTestQuestion = (RefQuestionAnswer)testQuestion;
            Navigation.PushAsync(new QuestionsEditor(selectedTestQuestion.QuestionAnswer.Questions));
        }

        private void Del(object testQuestion)
        {
            var selectedTestQuestion = (RefQuestionAnswer)testQuestion;

            viewModelManager.DeleteQuestionAnswerData(selectedTestQuestion.QuestionAnswer);

            DisplayAlert( AppResources.��������������, selectedTestQuestion.QuestionAnswer.Questions.QuestionName, AppResources.��);
            UpdateForm(CurrrentQuestions);
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

        public class RefQuestionAnswer
        { 
        public ExamModels.QuestionAnswer QuestionAnswer { get; set; }
        public Command EditCommand { get; set; }
        public Command DelCommand { get; set; }
         }

        private void ContentPageLoaded(object sender, EventArgs e)
        {

        }

        private void CreateButtonClicked(object sender, EventArgs e)
        {
            var refAnswerListPage = new RefAnswerListPage();
            refAnswerListPage.Mode = 1;
            refAnswerListPage.Disappearing += (s, args) =>
            {
                if (refAnswerListPage.vSelectedItem != null)
                {
                    var selectedItem = refAnswerListPage.vSelectedItem;
                    QuestionAnswer aQuestionQ = new QuestionAnswer();
                    aQuestionQ.Questions = CurrrentQuestions;
                    aQuestionQ.Answer = selectedItem;
                    viewModelManager.CreateQuestionAnswerData(aQuestionQ);

                    // Clear the selected item in RefQuestionsListPage
                    refAnswerListPage.vSelectedItem = null;

                    // Send a message to update the current form
#pragma warning disable CS0618 // ��� ��� ���� �������
                    MessagingCenter.Send(this, "UpdateForm");
#pragma warning restore CS0618 // ��� ��� ���� �������
                }
            };
            Navigation.PushModalAsync(refAnswerListPage);
        }
    } 
}