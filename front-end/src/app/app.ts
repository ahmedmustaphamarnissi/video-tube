import { Component, signal, HostListener, inject, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './components/header/header.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent, SidebarComponent],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  private readonly platformId = inject(PLATFORM_ID);

  /** Sidebar open by default on desktop, closed on mobile */
  sidebarOpen = signal(
    isPlatformBrowser(this.platformId) ? window.innerWidth > 640 : true
  );

  toggleSidebar(): void {
    this.sidebarOpen.update(v => !v);
  }

  closeSidebar(): void {
    this.sidebarOpen.set(false);
  }

  @HostListener('window:resize', ['$event'])
  onResize(event: UIEvent): void {
    const w = (event.target as Window).innerWidth;
    if (w > 640 && !this.sidebarOpen()) {
      this.sidebarOpen.set(true);
    }
  }
}
