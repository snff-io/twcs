// using System;
// using System.Collections.Generic;

// public class Program
// {
//     public static void Main()
//     {
//         // Build the menu structure
//         MenuItem mainMenu = new MenuItem("main");
//         MenuItem subMenu1 = new MenuItem("submenu1");
//         MenuItem subMenu2 = new MenuItem("submenu2");
//         MenuItem option1 = new MenuItem("option1", isPrompt: true); // Assuming this is a prompt for input
//         MenuItem option2 = new MenuItem("option2");

//         mainMenu.AddChild(subMenu1);
//         mainMenu.AddChild(subMenu2);
//         subMenu1.AddChild(option1);
//         subMenu2.AddChild(option2);

//         // Start the interactive navigation
//         InteractiveNavigate(mainMenu);
//     }

//     public static void InteractiveNavigate(MenuItem currentMenu)
//     {
//         while (true)
//         {
//             Console.WriteLine($"You are now at: {currentMenu.Name}");
//             if (currentMenu.IsPrompt)
//             {
//                 Console.Write("Please enter your value: ");
//                 string input = Console.ReadLine();
//                 Console.WriteLine($"You have entered: {input}");
//                 Console.WriteLine($"RESTful path: /{currentMenu.Name}/{input}");
//                 break; // End the interaction after input is provided
//             }
//             else if (currentMenu.Children.Count > 0)
//             {
//                 // Display options
//                 for (int i = 0; i < currentMenu.Children.Count; i++)
//                 {
//                     Console.WriteLine($"{i + 1}. {currentMenu.Children[i].Name}");
//                 }
//                 Console.Write("Choose an option: ");

//                 // Read and process user input
//                 if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= currentMenu.Children.Count)
//                 {
//                     currentMenu = currentMenu.Children[choice - 1]; // Navigate to the chosen menu
//                 }
//                 else
//                 {
//                     Console.WriteLine("Invalid choice, please try again.");
//                 }
//             }
//             else
//             {
//                 // No children and not a prompt, end of navigation
//                 Console.WriteLine("End of the line, no further options.");
//                 break;
//             }
//         }
//     }
// }
