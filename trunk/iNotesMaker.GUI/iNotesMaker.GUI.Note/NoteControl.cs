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
 
 using System;
 using Gtk;
 using System.Drawing;
 
 namespace iNotesMaker.GUI {
 	
 	public class Note : DrawingArea {
 		
		
		protected override bool OnButtonPressEvent ( Gdk.EventButton args ) {
			
			return true;
		}
		
		
		protected override bool OnMotionNotifyEvent( Gdk.EventMotion args ) {
	        	
			return true;	  
		}
		
		protected override bool OnConfigureEvent( Gdk.EventConfigure args ) {

			return true;	        	
		}
		
		protected override bool OnExposeEvent ( Gdk.EventExpose args ) {
			
			System.Drawing.Graphics gfx = Gtk.DotNet.Graphics.FromDrawable(args.Window);
			gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

	        	System.Drawing.Pen pen = new System.Drawing.Pen( System.Drawing.Color.Black );
			
	        	gfx.DrawLine(pen, 10,20,30,40);
	        	
	        	return true;
	        }  
		
 	}
 	
 }
 
 