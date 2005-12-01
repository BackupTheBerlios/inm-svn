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
 /* README README README README
 
 This is just a bunch of hacks to make it work at some state. The following things
 need fixing and adding are:
 	
 	1) Brush should be a circle and not a square
 	2) The lines should not overlap at their ends (if they do it)
 	3) Scroll bars should extend/scroll the Note *without* loosing the drawn stuff
 	4) *ALL* the drawing should be inside the OnExposeEvent - this means FillGap
 	must be recoded and the drawing put in the proper place.
 	5) Create a small dot cursor when writing.
 	6) Add support for the Eraser
 	7) Add the use of XInput for stylus pressure, etc
 	more?
 */
 using System;
 using Gtk;
 using System.Drawing;
 using Gdk;
 
 namespace iNotesMaker.GUI.Note {
 	
 	public class Control : DrawingArea {
 		
 		System.Drawing.Graphics gfx;
 		private int Eraser = 3;
 		private int Pointer = 1;
 		private Gdk.Pixmap pmNote = null;
 		
 		// Connects two points with a line
 		private struct FillLine {
 			
 			public static System.Drawing.Point StartPoint;
 			public static System.Drawing.Point EndPoint;
 			public static bool isNew = true;
 		}
 		
 		// Initalizes the masks
 		public Control() {
 			
 			 this.Events =  EventMask.ExposureMask | EventMask.LeaveNotifyMask | 
				       	       EventMask.ButtonPressMask | EventMask.PointerMotionMask | EventMask.ButtonReleaseMask |
				       	       EventMask.PointerMotionHintMask;
 		}
 		
 		
 		// Sets the starting point
		protected override bool OnButtonPressEvent ( Gdk.EventButton args ) {
			
			if( args.Button == Pointer ) {
				
				FillLine.StartPoint.X = (int) args.X;
				FillLine.StartPoint.Y = (int) args.Y;
				FillLine.isNew = false;
				DrawBrush (args.X, args.Y, true);
			}	
			
			
			//args.Window.Cursor = new Gdk.Cursor(Gdk.CursorType.Xterm);
			return true;
		}
		
		// resets points and state
		protected override bool OnButtonReleaseEvent ( Gdk.EventButton args ) {
			
			if( args.Button == Pointer ) {
				
				FillLine.StartPoint = System.Drawing.Point.Empty;
				FillLine.EndPoint = System.Drawing.Point.Empty;
				FillLine.isNew = true;
			}	
			
			
			//args.Window.Cursor = new Gdk.Cursor(Gdk.CursorType.Xterm);
			return true;
		}
		
		// Draws a little square as a brush
		void DrawBrush (double x, double y, bool black)
		{
			Gdk.Rectangle update_rect = new Gdk.Rectangle ();
			update_rect.X = (int) x - 1;
			update_rect.Y = (int) y - 1;
			update_rect.Width = 2;
			update_rect.Height = 2;
			
			pmNote.DrawRectangle (black ? this.Style.BlackGC : this.Style.WhiteGC, true,
										 update_rect.X, update_rect.Y,
										 update_rect.Width, update_rect.Height);
			this.QueueDrawArea (update_rect.X, update_rect.Y,
										update_rect.Width, update_rect.Height);
		}
		
		// Connects two points
		void FillGap ( Gdk.Window window ) {
			
			Gdk.Rectangle update_rect = new Gdk.Rectangle ();
			

			gfx = Gtk.DotNet.Graphics.FromDrawable( window );
			gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
		      	System.Drawing.Pen pen = new System.Drawing.Pen( System.Drawing.Color.Black );
			gfx.DrawLine(pen, FillLine.StartPoint, FillLine.EndPoint);	
		}
		
		// Track down the points with their coordinates via trackig mouse movement
		protected override bool OnMotionNotifyEvent( Gdk.EventMotion args ) {
			
			int x,y;
			Gdk.ModifierType state;
			
			// Hint means that it is a special event and there will be no other event if we don't call
			// GetPointer. Also we save the state and use it for the points resolving.
			if ( args.IsHint ) {
				Gdk.ModifierType s;
				args.Window.GetPointer (out x, out y, out s);
				state = s;
			}
			else {	// normal mouse movement event
				
				x = (int) args.X;
				y = (int) args.Y;
				state = args.State;
			}
			
			// 
			if( FillLine.isNew == true && (state & Gdk.ModifierType.Button1Mask) != 0 ) {
					
				FillLine.StartPoint.X = x;
				FillLine.StartPoint.Y = y;					
				
				if( FillLine.EndPoint != System.Drawing.Point.Empty) {
					
					FillGap( args.Window );
				}
				
				FillLine.EndPoint = System.Drawing.Point.Empty;
				FillLine.isNew = false;
			}
			else if ( FillLine.isNew == false && (state & Gdk.ModifierType.Button1Mask) != 0 )  {
				
				FillLine.EndPoint.X = x;
				FillLine.EndPoint.Y = y;
				
				FillGap( args.Window );
				
				FillLine.isNew = false;
				FillLine.StartPoint = FillLine.EndPoint;
			}
				
			return true;	  
		}
		

		// This events is executed when the window is resized, minimized, overlapped etc..
		protected override bool OnConfigureEvent( Gdk.EventConfigure args ) {
			
			pmNote = new Gdk.Pixmap( args.Window, this.Allocation.Width, this.Allocation.Height, -1 );
			pmNote.DrawRectangle( this.Style.WhiteGC, true, 0, 0,
				  	      			      this.Allocation.Width, this.Allocation.Height );

			return true;	        	
		}
		
		// All drawing should be inside this event
		protected override bool OnExposeEvent ( Gdk.EventExpose args ) {
			
			args.Window.DrawDrawable (this.Style.BlackGC,
							pmNote,
							args.Area.X, args.Area.Y,
							args.Area.X, args.Area.Y,
							args.Area.Width, args.Area.Height);
			
	        	return true;
	        }  
		
 	}
 	
 }
 
 