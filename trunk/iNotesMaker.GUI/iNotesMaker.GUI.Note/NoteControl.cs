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
 	
 	public class Note : Gtk.DrawingArea {
 		
 		/* START TEST CODE */
		public Note ()
		{
			SetSizeRequest (200, 200);
		}
				       
		protected override bool OnExposeEvent (Gdk.EventExpose args)
		{
			using (Graphics g = Gtk.DotNet.Graphics.FromDrawable (args.Window)){
				g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
				Pen p = new Pen (Color.Blue, 1.0f);

				for (int i = 0; i < 600; i += 60)
					for (int j = 0; j < 600; j += 60)
						g.DrawLine (p, i, 0, 0, j);
			}
			return true;
		}
		/* END TEST CODE */
 	
 	}
 	
 }
 