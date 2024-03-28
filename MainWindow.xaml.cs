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

namespace AnimalPairingGame;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        SetUpGame();
    }

    private void SetUpGame()
    {
        List<string> animalEmoji = new List<string>
        {
            "🐶", "🐶",
            "🐱", "🐱",
            "🐭", "🐭",
            "🐹", "🐹",
            "🐰", "🐰",
            "🦊", "🦊",
            "🐻", "🐻",
            "🐼", "🐼"
        };
        Random random = new Random(); // create a new random number generator

        Grid mainGrid = this.Content as Grid; // get the main grid from the window
        foreach(TextBlock textblock in mainGrid.Children.OfType<TextBlock>()) // loop through all the textblocks in the grid
        {
            int index = random.Next(animalEmoji.Count); //Pick a random number between 0 and the number of emoji left in the list and call it index
            string nextEmoji = animalEmoji[index]; //Use the random number called index to get a random emoji from the list
            textblock.Text = nextEmoji; // Update the TextBlock with the random emoji from the list
            animalEmoji.RemoveAt(index); // Remove the emoji from the list so it can't be used again
        }
    }
}