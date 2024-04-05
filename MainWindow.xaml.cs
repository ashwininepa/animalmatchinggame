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
using System.Windows.Threading;

namespace AnimalPairingGame;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    DispatcherTimer timer = new DispatcherTimer();
    int tenthsOfSecondsElapsed;
    int matchesFound;
    public MainWindow()
    {
        InitializeComponent();

        timer.Interval = TimeSpan.FromSeconds(0.1);
        timer.Tick += Timer_Tick;

        SetUpGame();
    }

    private void SetUpGame()
    {
        List<string> animalEmoji = new List<string>
        {
            "🐶", "🐶",
            "🐱", "🐱",
            "🐰", "🐰",
            "🦁", "🦁",
            "🐔", "🐔",
            "🐧", "🐧",
            "🐤", "🐤",
            "🦉", "🦉"
        };

        Random random = new Random(); // create a new random number generator

        foreach (TextBlock textblock in mainGrid.Children.OfType<TextBlock>()) // loop through all the textblocks in the grid
        {
            if(textblock.Name != "timeTextBlock")
            {
                textblock.Visibility = Visibility.Visible;
                int index = random.Next(animalEmoji.Count); //Pick a random number between 0 and the number of emoji left in the list and call it index
                string nextEmoji = animalEmoji[index]; //Use the random number called index to get a random emoji from the list
                textblock.Text = nextEmoji; // Update the TextBlock with the random emoji from the list
                animalEmoji.RemoveAt(index); // Remove the emoji from the list so it can't be used again
            }
            timer.Start();
            tenthsOfSecondsElapsed = 0;
            matchesFound = 0;
        }
    }
    private TextBlock lastTextBlockClicked;
    private bool findingMatch = false;        
    private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
    {
        TextBlock textBlock = sender as TextBlock;
        if (findingMatch == false)
        {
            textBlock.Visibility = Visibility.Hidden;
            lastTextBlockClicked = textBlock;
            findingMatch = true;
        }
        else if(textBlock.Text == lastTextBlockClicked.Text)
        {
            matchesFound++;
            textBlock.Visibility = Visibility.Hidden;
            findingMatch = false;
        }
        else
        {
            lastTextBlockClicked.Visibility = Visibility.Visible;
            findingMatch = false;
        }
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
            tenthsOfSecondsElapsed++;
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
            }
    }

    private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if(matchesFound == 8)
        {
            SetUpGame();
        }
    }
}