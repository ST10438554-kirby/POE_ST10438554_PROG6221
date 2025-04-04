using System;
using System.ComponentModel.Design;
using System.Media;
using System.Threading;

// Cybersecurity Awareness Bot - A beginner-friendly chatbot to educate users about cybersecurity
class CybersecurityAwarenessBot
{
    static void Main(string[] args)
    {
        // Display the ASCII art logo for the bot
        DisplayAsciiArt();

        // Get the user's name and greet them
        string userName = GetUserName();

        // Start the chatbot interaction loop
        StartChatLoop(userName);
    }
    // Method to play the welcome audio
    static void PlayWelcomeAudio()
    {
        try
        {
            using (SoundPlayer player = new SoundPlayer("awareness bot.wav"))
            {
                player.Load();  // Optional, to preload
                player.Play();  // Asynchronously play while ASCII art displays
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error playing sound: " + ex.Message);
            Console.ResetColor();
        }
    }

    // Method to display ASCII art logo/header
    static void DisplayAsciiArt()
    {
        Console.ForegroundColor = ConsoleColor.Green;// ascii art is green
        Console.WriteLine(@"
 
  ______             __                             ______                                           __    __                       ______                                                                                             _______               __     
 /      \           |  \                           /      \                                         |  \  |  \                     /      \                                                                                           |       \             |  \    
|  $$$$$$\ __    __ | $$____    ______    ______  |  $$$$$$\  ______    _______  __    __   ______   \$$ _| $$_    __    __       |  $$$$$$\ __   __   __   ______    ______    ______   _______    ______    _______   _______       | $$$$$$$\  ______   _| $$_   
| $$   \$$|  \  |  \| $$    \  /      \  /      \ | $$___\$$ /      \  /       \|  \  |  \ /      \ |  \|   $$ \  |  \  |  \      | $$__| $$|  \ |  \ |  \ |      \  /      \  /      \ |       \  /      \  /       \ /       \      | $$__/ $$ /      \ |   $$ \  
| $$      | $$  | $$| $$$$$$$\|  $$$$$$\|  $$$$$$\ \$$    \ |  $$$$$$\|  $$$$$$$| $$  | $$|  $$$$$$\| $$ \$$$$$$  | $$  | $$      | $$    $$| $$ | $$ | $$  \$$$$$$\|  $$$$$$\|  $$$$$$\| $$$$$$$\|  $$$$$$\|  $$$$$$$|  $$$$$$$      | $$    $$|  $$$$$$\ \$$$$$$  
| $$   __ | $$  | $$| $$  | $$| $$    $$| $$   \$$ _\$$$$$$\| $$    $$| $$      | $$  | $$| $$   \$$| $$  | $$ __ | $$  | $$      | $$$$$$$$| $$ | $$ | $$ /      $$| $$   \$$| $$    $$| $$  | $$| $$    $$ \$$    \  \$$    \       | $$$$$$$\| $$  | $$  | $$ __ 
| $$__/  \| $$__/ $$| $$__/ $$| $$$$$$$$| $$      |  \__| $$| $$$$$$$$| $$_____ | $$__/ $$| $$      | $$  | $$|  \| $$__/ $$      | $$  | $$| $$_/ $$_/ $$|  $$$$$$$| $$      | $$$$$$$$| $$  | $$| $$$$$$$$ _\$$$$$$\ _\$$$$$$\      | $$__/ $$| $$__/ $$  | $$|  \
 \$$    $$ \$$    $$| $$    $$ \$$     \| $$       \$$    $$ \$$     \ \$$     \ \$$    $$| $$      | $$   \$$  $$ \$$    $$      | $$  | $$ \$$   $$   $$ \$$    $$| $$       \$$     \| $$  | $$ \$$     \|       $$|       $$      | $$    $$ \$$    $$   \$$  $$                                                                                                          
  \$$$$$$  _\$$$$$$$ \$$$$$$$   \$$$$$$$ \$$        \$$$$$$   \$$$$$$$  \$$$$$$$  \$$$$$$  \$$       \$$    \$$$$  _\$$$$$$$       \$$   \$$  \$$$$$\$$$$   \$$$$$$$ \$$        \$$$$$$$ \$$   \$$  \$$$$$$$ \$$$$$$$  \$$$$$$$        \$$$$$$$   \$$$$$$     \$$$$
          |  \__| $$                                                                                              |  \__| $$                                                                                                                                       
           \$$    $$                                                                                               \$$    $$                                                                                                                                       
            \$$$$$$                                                                                                 \$$$$$$                                                                                                                                       

");
        Console.ForegroundColor = ConsoleColor.Cyan; // Astronaut blue color
        Console.WriteLine("    Cybersecurity Awareness Bot                          ");
        Console.WriteLine("=======================================================");
        Console.ResetColor(); // Reset text color to default
    }

    // Method to get the user's name and greet them
    static string GetUserName()
    {
        Console.Write("\nPlease enter your name: ");
        string name = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(name))
        {
            Console.Write("Name cannot be empty. Please enter your name: ");
            name = Console.ReadLine();
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\nWelcome, {name}! Let's talk about staying safe online.\n");
        Console.ResetColor();
        return name;
    }

    // Main chatbot loop
    static void StartChatLoop(string userName)
    {
        string input = "";
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{userName}, what would you like to know about cybersecurity? (type 'exit' to quit): ");
            Console.ResetColor();
            input = Console.ReadLine()?.ToLower();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine(" I didn’t quite understand that. Could you rephrase?");
            }
            else if (input == "exit")
            {
                Console.WriteLine("\nLogging out");
                for (int i = 0; i < 3; i++)
                {
                    Thread.Sleep(500);
                    Console.Write(".");
                }
                Console.WriteLine("\n Goodbye! Stay safe online.");
                break;
            }
            else if (input.Contains("how are you"))
            {
                TypeEffect("I'm just a bot, but I'm here to help you stay secure!");
            }
            else if (input.Contains("purpose"))
            {
                TypeEffect("My purpose is to provide you with cybersecurity tips and knowledge.");
            }
            else if (input.Contains("password"))
            {
                TypeEffect(" Always use strong, unique passwords and consider using a password manager.");
            }
            else if (input.Contains("phishing"))
            {
                TypeEffect(" Watch out for phishing emails. Never click on suspicious links or attachments.");
            }
            else if (input.Contains("safe browsing"))
            {
                TypeEffect("Always ensure websites are HTTPS secured and avoid sharing personal data on unsecured sites.");
            }
            else if (input.Contains("2fa") || input.Contains("two-factor"))
            {
                TypeEffect(" Enable Two-Factor Authentication (2FA) to add an extra layer of security to your accounts.");
            }
            else if (input.Contains("vpn"))
            {
                TypeEffect(" A VPN encrypts your internet traffic, keeping your online activities private and secure.");
            }
            else
            {
                Console.WriteLine(" I'm not sure about that topic. Try asking about 'passwords', 'phishing', '2FA', or 'VPNs'.");
            }
        }
    }

    // Typing effect
    static void TypeEffect(string message)
    {
        foreach (char c in message)
        {
            Console.Write(c);
            Thread.Sleep(30);
        }
        Console.WriteLine();
    }
}
