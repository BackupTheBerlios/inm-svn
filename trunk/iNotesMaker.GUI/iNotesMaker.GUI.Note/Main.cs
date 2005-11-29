// project created on 11/29/2005 at 06:42
using System;
using Gtk;

class MainClass
{
	public static void Main (string[] args)
	{
		Application.Init ();
		new MyWindow ();
		Application.Run ();
	}
}