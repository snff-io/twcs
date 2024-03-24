// using System;
// using Terminal.Gui;

// class Program
// {
//     static void Main(string[] args)
//     {
//         Application.Init();

//         // Define color scheme
//         var colorScheme = new ColorScheme
//         {
//             Normal = new Terminal.Gui.Attribute(Color.DarkGray, Color.Black), // Background color #333333
//             Focus = new Terminal.Gui.Attribute(Color.DarkGray, Color.Black),
//             HotNormal = new Terminal.Gui.Attribute(Color.DarkGray, Color.Black),
//             HotFocus = new Terminal.Gui.Attribute(Color.DarkGray, Color.Black),
//             Disabled = new Terminal.Gui.Attribute(Color.DarkGray, Color.Black),
//         };

//         // Create a window to host the grid
//         var top = Application.Top;
//         var win = new Window("Adjustable Panels")
//         {
//             X = 0,
//             Y = 0,
//             Width = Dim.Fill(),
//             Height = Dim.Fill(),
//             ColorScheme = colorScheme
//         };
//         top.Add(win);

//         // Create a grid layout
//         var grid = new GridView()
//         {
//             X = 0,
//             Y = 0,
//             Width = Dim.Fill(),
//             Height = Dim.Fill(),
//             ColorScheme = colorScheme
//         };
//         win.Add(grid);

//         // Add panels to the grid
//         for (int i = 0; i < 3; i++)
//         {
//             for (int j = 0; j < 2; j++)
//             {
//                 var panel = new FrameView($"Panel {i * 2 + j + 1}")
//                 {
//                     Width = Dim.Fill() / 2, // Each panel takes half of the grid's width
//                     Height = Dim.Fill() / 3, // Each panel takes one-third of the grid's height
//                     ColorScheme = colorScheme
//                 };
//                 grid.Add(panel, i, j); // Add panel to the grid at position (i, j)
//             }
//         }

//         // Handle terminal resizing
//         Application.Resized += (e) =>
//         {
//             // Resize grid to fill the entire window
//             grid.Width = Dim.Fill();
//             grid.Height = Dim.Fill();
//         };

//         Application.Run();
//     }
// }
