using ExamClient.Resources.Resx;
using ExamModels;
using static System.Net.Mime.MediaTypeNames;

namespace Client.Users.Doc.DocTestQuestionsTheAnswers;

public partial class DocTestQuestionsTheAnswers : ContentPage
{
   // private List<DocTestMenu.DocTestMenu.RefTestQuestion> refTestQuestions;

    public CommandCL command = new CommandCL();
    private TestQuestionEditorViewModel viewModel;
    private TestQuestionManager viewModelManager;
    private ExamModels.Test CurrrentTest;
    private ExamModels.Exams Exams;
    private ExamModels.User CurrrentUser;
    public List<ExamModels.Questions> questions1 = new List<Questions>();

    public DocTestQuestionsTheAnswers(ExamModels.Test refTestQuestions , ExamModels.Exams exams, ExamModels.User curentUsers)
	{
       

        InitializeComponent();
        viewModel = new TestQuestionEditorViewModel();
        viewModelManager = new TestQuestionManager();
        CurrrentTest = refTestQuestions;
        Exams = exams;
        CurrrentUser = curentUsers;
        TestList.ItemsSource = GetTestQuestions(refTestQuestions);
        Title = refTestQuestions.Name_Test;

        //TestName.Text = refTestQuestions.Name_Test;
#pragma warning disable CS0618 // ��� ��� ���� �������
        MessagingCenter.Subscribe<DocTestQuestionsTheAnswers>(this, "UpdateForm", (sender) =>
        {
            // Perform the necessary updates to the form here
            // For example, update the fields, refresh data, etc.
            TestList.ItemsSource = GetTestQuestions(refTestQuestions);
        });
#pragma warning restore CS0618 // ��� ��� ���� �������
    }

 
    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null)
            return;

        var selectedTestQuestion = (RefTestQuestion)e.SelectedItem;

        //    await DisplayAlert("��������� ������", selectedTestQuestion.TestQuestion.IdQuestions.QuestionName, "OK");
            // var selectedTestQuestion = (RefQuestionAnswer)e.SelectedItem;
            await DisplayAlert(AppResources.��������������, selectedTestQuestion.TestQuestion.IdQuestions.QuestionName, AppResources.��);
      //  questions1.Add(selectedTestQuestion.TestQuestion.IdQuestions);

        if (!questions1.Any(q => q.QuestionName == selectedTestQuestion.TestQuestion.IdQuestions.QuestionName))
        {
            questions1.Add(selectedTestQuestion.TestQuestion.IdQuestions);
        }

        await Navigation.PushAsync(new Doc.DocAnswerQuestins.DocAnswerQuestins(selectedTestQuestion.TestQuestion.IdQuestions, CurrrentTest, Exams, CurrrentUser,questions1));

        ((ListView)sender).SelectedItem = null;

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
                var refTestQuestion = new RefTestQuestion { TestQuestion = CommandCL.TestQuestionListGet.ListTestQuestion[i] };
                testQuestionList.Add(refTestQuestion);
            }
        }

        return testQuestionList;
    }

    public class RefTestQuestion
    {
        public ExamModels.TestQuestion TestQuestion { get; set; }
        public Command EditCommand { get; set; }
        public Command DelCommand { get; set; }
    }


    public class RefQuestionAnswer
    {
        public ExamModels.QuestionAnswer QuestionAnswer { get; set; }
        public Command EditCommand { get; set; }
        public Command DelCommand { get; set; }
    }

}