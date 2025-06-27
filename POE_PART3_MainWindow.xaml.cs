
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Documents;

namespace CybersecurityAwarenessBot
{
    public class CyberTask
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReminderDate { get; set; }
        public bool IsCompleted { get; set; }
    }

    public partial class MainWindow : Window
    {
        private List<string> activityLog = new();
        private List<string> quizQuestions = new();
        private Dictionary<string, string> quizAnswers = new();
        private int currentQuizIndex = 0;
        private int quizScore = 0;
        private List<CyberTask> taskList = new();

        // New fields for 2FA simulation
        private string generated2FACode = "";
        private bool awaiting2FACode = false;

        public MainWindow()
        {
            InitializeComponent();
            InitializeQuiz();
        }

        private void UserInput_GotFocus(object sender, RoutedEventArgs e)
        {
            if (UserInput.Text == "Type your message here...")
            {
                UserInput.Text = "";
                UserInput.Foreground = Brushes.White;
            }
        }

        private void UserInput_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UserInput.Text))
            {
                UserInput.Text = "Type your message here...";
                UserInput.Foreground = Brushes.Gray;
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string input = UserInput.Text.Trim();
            if (input == "Type your message here..." || string.IsNullOrEmpty(input)) return;

            AppendChat("You: " + input);
            HandleUserInput(input);
            UserInput.Text = "";
            UserInput_LostFocus(null, null);
        }

        private void HandleUserInput(string input)
        {
            string lower = input.ToLower();
            DateTime? reminderDate = ReminderDatePicker.SelectedDate;

            var matchInDays = Regex.Match(input, @"remind me in (\\d+) days", RegexOptions.IgnoreCase);
            if (matchInDays.Success)
            {
                int days = int.Parse(matchInDays.Groups[1].Value);
                reminderDate = DateTime.Now.AddDays(days);
            }

            // Handle 2FA code input if awaiting verification
            if (awaiting2FACode)
            {
                if (input.Trim() == generated2FACode)
                {
                    AppendChat("✅ Chatbot: 2FA verified successfully.");
                    activityLog.Add("2FA verification successful.");
                    awaiting2FACode = false;
                    generated2FACode = "";
                }
                else
                {
                    AppendChat("❌ Chatbot: Incorrect verification code. Try again.");
                }
                return; // Stop further processing while awaiting 2FA input
            }

            // Command to enable 2FA
            if (lower.Contains("enable 2fa") || lower.Contains("two-factor authentication"))
            {
                generated2FACode = new Random().Next(1000, 9999).ToString();
                awaiting2FACode = true;
                AppendChat($"Chatbot: Two-Factor Authentication enabled. Your verification code is: {generated2FACode}. Please enter it to confirm.");
                activityLog.Add("2FA initiated.");
                return;
            }

            if (lower.StartsWith("add task") || lower.StartsWith("remind me") || lower.StartsWith("set reminder"))
            {
                string title = ExtractTaskTitle(input);
                CyberTask newTask = new()
                {
                    Title = title,
                    Description = title,
                    ReminderDate = reminderDate,
                    IsCompleted = false
                };

                taskList.Add(newTask);

                string response = $"Task added: '{newTask.Title}'";
                if (reminderDate.HasValue)
                {
                    response += $" | Reminder set for {reminderDate.Value.ToShortDateString()}";
                }

                activityLog.Add(response);
                AppendChat("Chatbot: " + response);
            }
            else if (lower.StartsWith("view tasks"))
            {
                if (taskList.Count == 0)
                {
                    AppendChat("Chatbot: You have no tasks yet.");
                }
                else
                {
                    AppendChat("Chatbot: Here are your tasks:");
                    foreach (var task in taskList)
                    {
                        string status = task.IsCompleted ? "✅ Done" : "🕒 Pending";
                        string reminder = task.ReminderDate.HasValue ? $" | Reminder: {task.ReminderDate.Value.ToShortDateString()}" : "";
                        AppendChat($"- {task.Title}{reminder} [{status}]");
                    }
                }
            }
            else if (lower.StartsWith("complete task"))
            {
                string taskName = input.Substring("complete task".Length).Trim();
                var task = taskList.FirstOrDefault(t => t.Title.ToLower() == taskName.ToLower());

                if (task != null)
                {
                    task.IsCompleted = true;
                    AppendChat($"Chatbot: Task '{task.Title}' marked as completed ✅");
                    activityLog.Add($"Task completed: {task.Title}");
                }
                else
                {
                    AppendChat("Chatbot: Could not find that task.");
                }
            }
            else if (lower.StartsWith("delete task"))
            {
                string taskName = input.Substring("delete task".Length).Trim();
                var task = taskList.FirstOrDefault(t => t.Title.ToLower() == taskName.ToLower());

                if (task != null)
                {
                    taskList.Remove(task);
                    AppendChat($"Chatbot: Task '{task.Title}' has been deleted ");
                    activityLog.Add($"Task deleted: {task.Title}");
                }
                else
                {
                    AppendChat("Chatbot: Could not find that task.");
                }
            }
            else if (lower.Contains("quiz") || lower.Contains("play game"))
            {
                StartQuiz();
            }
            else if (lower.Contains("what have you done") || lower.Contains("activity log"))
            {
                ShowActivityLog();
            }
            else if (quizQuestions.Any() && currentQuizIndex < quizQuestions.Count)
            {
                EvaluateQuizAnswer(input);
            }
            else
            {
                AppendChat("Chatbot: Sorry, I didn’t understand that. Try 'add task', 'view tasks', 'complete task', 'delete task', 'start quiz', or 'view log'.");
            }
        }

        private string ExtractTaskTitle(string input)
        {
            var match = Regex.Match(input, @"add (a )?task (to|for)? (?<task>.+)", RegexOptions.IgnoreCase);
            if (match.Success) return match.Groups["task"].Value;

            match = Regex.Match(input, @"remind me to (?<task>.+)", RegexOptions.IgnoreCase);
            if (match.Success) return match.Groups["task"].Value;

            return "Untitled Task";
        }

        private void AppendChat(string message)
        {
            Paragraph paragraph = new Paragraph();

            if (message.StartsWith("You: "))
                paragraph.Inlines.Add(new Run(message + "\n") { Foreground = Brushes.Blue });
            else if (message.StartsWith("Chatbot: "))
                paragraph.Inlines.Add(new Run(message + "\n") { Foreground = Brushes.Green });
            else if (message.StartsWith("✅") || message.StartsWith("❌") || message.StartsWith("🎉"))
                paragraph.Inlines.Add(new Run(message + "\n") { Foreground = Brushes.DarkOrange });
            else
                paragraph.Inlines.Add(new Run(message + "\n") { Foreground = Brushes.Black });

            ChatDisplay.Document.Blocks.Add(paragraph);
            ChatDisplay.ScrollToEnd();
        }

        private void ViewActivityLog_Click(object sender, RoutedEventArgs e)
        {
            ShowActivityLog();
        }

        private void ShowActivityLog()
        {
            AppendChat("Chatbot: Here's your recent activity:");
            foreach (var action in activityLog.TakeLast(5))
                AppendChat("- " + action);
        }

        private void StartQuiz_Click(object sender, RoutedEventArgs e)
        {
            StartQuiz();
        }

        private void StartQuiz()
        {
            quizScore = 0;
            currentQuizIndex = 0;
            AppendChat("Chatbot: Quiz started! Type A, B, C, or D to answer.");
            AskNextQuizQuestion();
            activityLog.Add("Quiz started");
        }

        private void AskNextQuizQuestion()
        {
            if (currentQuizIndex < quizQuestions.Count)
            {
                AppendChat($"Q{currentQuizIndex + 1}: {quizQuestions[currentQuizIndex]}");
            }
            else
            {
                EndQuiz();
            }
        }

        private void InitializeQuiz()
        {
            var questions = new Dictionary<string, string>
            {
                ["What should you do if you receive an email asking for your password?\nA) Reply\nB) Delete\nC) Report it\nD) Ignore it"] = "C",
                ["Which of the following is strongest?\nA) password123\nB) qwerty\nC) P@ssw0rd!\nD) 123456"] = "C",
                ["What is phishing?\nA) A sport\nB) Virus\nC) Scam emails\nD) Anti-virus tool"] = "C",
                ["What is 2FA?\nA) Two friends\nB) Double account\nC) Extra login step\nD) Browser setting"] = "C",
                ["Should you use public Wi-Fi for banking?\nA) Yes\nB) Only at home\nC) Only with VPN\nD) Never"] = "C",
                ["What does a firewall do?\nA) Heats your computer\nB) Blocks unauthorized access\nC) Stores data\nD) Monitors screen"] = "B",
                ["True or False: Sharing your password with friends is safe."] = "False",
                ["What is social engineering?\nA) Building design\nB) Software\nC) Human-based hacking\nD) Internet engineering"] = "C",
                ["True or False: It is safe to click unknown links in emails."] = "False",
                ["What should you do if your device is infected?\nA) Ignore it\nB) Reboot\nC) Run antivirus scan\nD) Share files"] = "C"
            };

            foreach (var q in questions)
            {
                quizQuestions.Add(q.Key);
                quizAnswers[q.Key] = q.Value;
            }
        }

        private void EvaluateQuizAnswer(string userAnswer)
        {
            if (currentQuizIndex < quizQuestions.Count)
            {
                string question = quizQuestions[currentQuizIndex];
                string correct = quizAnswers[question];

                if (userAnswer.Trim().Equals(correct, StringComparison.OrdinalIgnoreCase))
                {
                    AppendChat(" Correct!");
                    quizScore++;
                }
                else
                {
                    AppendChat($" Incorrect. The correct answer was: {correct}");
                }

                currentQuizIndex++;
                AskNextQuizQuestion();
            }
            else
            {
                EndQuiz();
            }
        }

        private void EndQuiz()
        {
            AppendChat($"🎉 Quiz finished! Your score: {quizScore}/{quizQuestions.Count}");

            string feedback = quizScore >= 8 ?
                "You're a cybersecurity pro!" :
                quizScore >= 5 ?
                "Not bad! Keep practicing and improving your knowledge." :
                "Looks like you need to study more. Stay safe online!";

            AppendChat("Chatbot: " + feedback);
            activityLog.Add($"Quiz completed. Score: {quizScore}/{quizQuestions.Count}");
        }
    }
}
