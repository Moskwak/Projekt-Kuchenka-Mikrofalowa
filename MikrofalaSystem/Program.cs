// See https://aka.ms/new-console-template for more information

using MikrofalaSystem.UI;

ScreenViewer viewer = new();

viewer.AddToStack(new ScreenStart(viewer));

viewer.RunUntilDone();