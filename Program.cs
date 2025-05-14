using System;
using System.Media;
using System.Threading;

//Cybersecurity Awareness Bot 
class CybersecurityAwarenessBot
{
    static void Main(string[] args)
    {
        Console.Title = "Cybersecurity Awareness Bot";

        // Display ASCII
        DisplayAsciiArt();

        // Plays greeting sound
        PlayWelcomeAudio();

        // Ask for user's name
        string userName = GetUserName();

        //chatbot with user interaction
        StartChatLoop(userName);
    }

    // Method to play a welcome audio
    static void PlayWelcomeAudio()
    {
        try
        {
            // Path to the audio file
            string audioFilePath = @"C:\Users\RC_Student_lab\Desktop\POE_PART2_ST10438554\greeting\greeting.wav";

            // Use SoundPlayer to play the greeting
            using (SoundPlayer player = new SoundPlayer(audioFilePath))
            {
                player.Load();  // Load the audio file
                player.Play();  // Play the audio
            }
        }
        catch (Exception ex)
        {
            // error message
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error playing sound: " + ex.Message);
            Console.ResetColor();
        }
    }

    //displays ASCII banner
    static void DisplayAsciiArt()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(@"   _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _ 
|_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_|
|_|                                                                                                                                             |_|
|_|  ______             __                                                                              __    __                                |_|
|_| /      \           /  |                                                                            /  |  /  |                               |_|
|_|/$$$$$$  | __    __ $$ |____    ______    ______    _______   ______    _______  __    __   ______  $$/  _$$ |_    __    __                  |_|
|_|$$ |  $$/ /  |  /  |$$      \  /      \  /      \  /       | /      \  /       |/  |  /  | /      \ /  |/ $$   |  /  |  /  |                 |_|
|_|$$ |      $$ |  $$ |$$$$$$$  |/$$$$$$  |/$$$$$$  |/$$$$$$$/ /$$$$$$  |/$$$$$$$/ $$ |  $$ |/$$$$$$  |$$ |$$$$$$/   $$ |  $$ |                 |_|
|_|$$ |   __ $$ |  $$ |$$ |  $$ |$$    $$ |$$ |  $$/ $$      \ $$    $$ |$$ |      $$ |  $$ |$$ |  $$/ $$ |  $$ | __ $$ |  $$ |                 |_|
|_|$$ \__/  |$$ \__$$ |$$ |__$$ |$$$$$$$$/ $$ |       $$$$$$  |$$$$$$$$/ $$ \_____ $$ \__$$ |$$ |      $$ |  $$ |/  |$$ \__$$ |                 |_|
|_|$$    $$/ $$    $$ |$$    $$/ $$       |$$ |      /     $$/ $$       |$$       |$$    $$/ $$ |      $$ |  $$  $$/ $$    $$ |                 |_|
|_| $$$$$$/   $$$$$$$ |$$$$$$$/   $$$$$$$/ $$/       $$$$$$$/   $$$$$$$/  $$$$$$$/  $$$$$$/  $$/       $$/    $$$$/   $$$$$$$ |                 |_|
|_|          /  \__$$ |                                                                                              /  \__$$ |                 |_|
|_|          $$    $$/                                                                                               $$    $$/                  |_|
|_|           $$$$$$/                                                                                                 $$$$$$/                   |_|
|_|  ______                                                                                            _______               __                 |_|
|_| /      \                                                                                          /       \             /  |                |_|
|_|/$$$$$$  | __   __   __   ______    ______    ______   _______    ______    _______  _______       $$$$$$$  |  ______   _$$ |_               |_|
|_|$$ |__$$ |/  | /  | /  | /      \  /      \  /      \ /       \  /      \  /       |/       |      $$ |__$$ | /      \ / $$   |              |_|
|_|$$    $$ |$$ | $$ | $$ | $$$$$$  |/$$$$$$  |/$$$$$$  |$$$$$$$  |/$$$$$$  |/$$$$$$$//$$$$$$$/       $$    $$< /$$$$$$  |$$$$$$/               |_|
|_|$$$$$$$$ |$$ | $$ | $$ | /    $$ |$$ |  $$/ $$    $$ |$$ |  $$ |$$    $$ |$$      \$$      \       $$$$$$$  |$$ |  $$ |  $$ | __             |_|
|_|$$ |  $$ |$$ \_$$ \_$$ |/$$$$$$$ |$$ |      $$$$$$$$/ $$ |  $$ |$$$$$$$$/  $$$$$$  |$$$$$$  |      $$ |__$$ |$$ \__$$ |  $$ |/  |            |_|
|_|$$ |  $$ |$$   $$   $$/ $$    $$ |$$ |      $$       |$$ |  $$ |$$       |/     $$//     $$/       $$    $$/ $$    $$/   $$  $$/             |_|
|_|$$/   $$/  $$$$$/$$$$/   $$$$$$$/ $$/        $$$$$$$/ $$/   $$/  $$$$$$$/ $$$$$$$/ $$$$$$$/        $$$$$$$/   $$$$$$/     $$$$/              |_|
|_| _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _ |_|
|_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_||_| 
 ");

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("    Cybersecurity Awareness Bot");
        Console.WriteLine("=======================================================");
        Console.ResetColor(); // Restore console text color
    }

    // Method to prompt and validate the user's name
    static string GetUserName()
    {
        Console.Write("\nPlease enter your name: ");
        string name = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(name))
        {
            Console.Write("Name cannot be empty. Please enter your name: ");
            name = Console.ReadLine();
        }

        // Welcome the user
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\nWelcome, {name}! Let's talk about staying safe online.\n");
        Console.ResetColor();

        return name;
    }

    // Chatbot responds to user input
    static void StartChatLoop(string userName)
    {
        string input;

        while (true)
        {
            // Prompt the user for a question
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{userName}, what would you like to know about cybersecurity? (type 'exit' to quit): ");
            Console.ResetColor();

            input = Console.ReadLine()?.ToLower(); // Normalize input to lowercase

            // Handle different inputs
            if (string.IsNullOrWhiteSpace(input))
            {
                DisplayBotResponse("I didn’t quite understand that. Could you rephrase?");
            }
            else if (input == "exit")
            {
                Farewell();
                break;
            }
            else if (input.Contains("how are you"))
            {
                DisplayBotResponse("I'm just a bot, but I'm here to help you stay secure!");
            }
            else if (input.Contains("purpose"))
            {
                DisplayBotResponse("My purpose is to provide you with cybersecurity tips and knowledge.");
            }
            else if (input.Contains("password"))
            {
                // Respond with password tips
                DisplayBotResponse(" Use strong passwords with a mix of letters, numbers, and symbols.");
                DisplayBotResponse(" Avoid reusing passwords across different accounts.");
                DisplayBotResponse(" Consider using a trusted password manager.");
            }
            else if (input.Contains("phishing"))
            {
                // Respond with phishing protection tips
                DisplayBotResponse(" Be cautious of emails asking for personal information.");
                DisplayBotResponse(" Never click suspicious links or download unknown attachments.");
                DisplayBotResponse(" Verify sender email addresses carefully.");
            }
            else if (input.Contains("safe browsing"))
            {
                // Respond with safe browsing tips
                DisplayBotResponse(" Make sure websites use HTTPS.");
                DisplayBotResponse(" Don’t enter sensitive information on unsecured websites.");
            }
            else if (input.Contains("2fa") || input.Contains("two-factor"))
            {
                // Respond with 2FA tips
                DisplayBotResponse(" Two-Factor Authentication adds an extra layer of security.");
                DisplayBotResponse(" Use apps like Google Authenticator or receive SMS codes.");
            }
            else if (input.Contains("vpn"))
            {
                // Respond with VPN tips
                DisplayBotResponse(" A VPN hides your IP and encrypts your data.");
                DisplayBotResponse(" Use it on public Wi-Fi for better protection.");
            }
            else
            {
                // Default if no match found
                DisplayBotResponse(" I'm not sure about that topic.");
                DisplayBotResponse("Try asking about 'passwords', 'phishing', '2FA', 'VPNs', or 'safe browsing'.");
            }
        }
    }

    //  typing effect
    static void DisplayBotResponse(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        foreach (char c in message)
        {
            Console.Write(c);
            Thread.Sleep(25); // Slow typing animation
        }
        Console.WriteLine();
        Console.ResetColor();
        Thread.Sleep(200); // Small delay after each message
    }

    static void Farewell()
    {
        Console.WriteLine("\nEXITING");
        for (int i = 0; i < 3; i++)
        {
            Thread.Sleep(500); // Pause for dots
            Console.Write(".");
        }

        // Final goodbye message
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\nGoodbye! Stay safe online and keep your data secure!");
        Console.ResetColor();
    }
}
