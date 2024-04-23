// using System;
// using System.Collections.Generic;
// using System.Linq;

// public class MenuItem
// {
//     public string Name { get; set; }
//     public List<MenuItem> Children { get; set; } = new List<MenuItem>();
//     public bool IsPrompt { get; set; } = false;
//     public Type PromptType { get; set; } = null;

//     public MenuItem(string name, bool isPrompt = false, Type promptType = null)
//     {
//         Name = name;
//         IsPrompt = isPrompt;
//         PromptType = promptType;
//     }

//     public void AddChild(MenuItem item)
//     {
//         Children.Add(item);
//     }
// }

// public class MenuSystem
// {
//     private MenuItem root;
//     private List<string> path = new List<string>();

//     public MenuSystem(MenuItem root)
//     {
//         this.root = root;
//     }

//     // Simulate user navigation through the menu
//     // pathParts: sequence of choices (integers) made by the user, last one can be a prompt input
//     public string NavigateAndGeneratePath(List<dynamic> pathParts)
//     {
//         path.Clear();
//         GeneratePathRecursive(root, pathParts, 0);
//         return "/" + string.Join("/", path);
//     }

//     private bool GeneratePathRecursive(MenuItem currentItem, List<dynamic> pathParts, int index)
//     {
//         if (index >= pathParts.Count) return false;

//         if (currentItem.IsPrompt)
//         {
//             path.Add(pathParts[index].ToString());
//             return true; // End of recursion on a prompt item
//         }
//         else
//         {
//             path.Add(currentItem.Name);
//             if (currentItem.Children.Count > 0 && index < pathParts.Count)
//             {
//                 int choice = pathParts[index];
//                 if (choice >= 0 && choice < currentItem.Children.Count)
//                 {
//                     return GeneratePathRecursive(currentItem.Children[choice], pathParts, index + 1);
//                 }
//             }
//         }
//         return false;
//     }
// }
