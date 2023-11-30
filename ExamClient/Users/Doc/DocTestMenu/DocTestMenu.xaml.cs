using ExamClient.Resources.Resx;
using ExamModels;

namespace Client.Users.Doc.DocTestMenu;

public partial class DocTestMenu : ContentPage
{
    public CommandCL command = new CommandCL();
    private TestQuestionEditorViewModel viewModel;
    private TestQuestionManager viewModelManager;
    private ExamModels.Test CurrrentTest;

    private ExamModels.Exams Exams;
    public  List<RefTestQuestion> refTestQuestions = new List<RefTestQuestion>();
    private ExamModels.User CurrrentUser;
    public DocTestMenu(ExamModels.Exams exams ,ExamModels.Test test, ExamModels.User currrentUser)
	{
		InitializeComponent();
        viewModel = new TestQuestionEditorViewModel();
        viewModelManager = new TestQuestionManager();
        TestName.Text = test.Name_Test;
        CurrrentTest = test;
        Exams =exams;
        CurrrentUser = currrentUser;
       var Result =   GetTestQuestions(test);
        refTestQuestions = Result;
       Question.Text = AppResources.Количествовопросов + Result.Count().ToString();
    

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
                var refTestQuestion = new RefTestQuestion { TestQuestion = CommandCL.TestQuestionListGet.ListTestQuestion[i]};
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


    private async void TestStart_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Doc.DocTestQuestionsTheAnswers.DocTestQuestionsTheAnswers(CurrrentTest, Exams,CurrrentUser));
     
    }
}