/*
 *  Copyright (C) 2005 Ivan N. Zlatev (contact@i-nz.net)
 *
 *  This program is free software; you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation; either version 2 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program; if not, write to the Free Software
 *  Foundation, Inc., 675 Mass Ave, Cambridge, MA 02139, USA.
 */
 
 using Glade;
 using Gtk;
 using System;
 using iNotesMaker.GUI;
 
 namespace iNotesMaker.GUI.Dialogs {
 	
 	public class MainWindow {
 		
 		[Widget] Gtk.Viewport viewportNote;
 		[Widget] Gtk.Window mainDialog;
 		
 		public MainWindow( ) {
 			
 			Glade.XML gxml = new Glade.XML (null, "gui.glade", "mainDialog", null);
 			gxml.Autoconnect (this);
 			
 			viewportNote.Add(new Note() );
 			
 			mainDialog.ShowAll();
 		}
 		
 		private void MainDialog_DeleteEvent( object o, Gtk.DeleteEventArgs args ) {
 			
 			Application.Quit();
 			args.RetVal = true;
 		}
 		
 		private void mnuQuit_Activate( object o, EventArgs args ) {
 			
 			Application.Quit();
 		}
 		
 	}
 	
 }
 
 		
 			
 
 