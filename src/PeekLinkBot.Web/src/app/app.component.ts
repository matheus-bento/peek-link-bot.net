import { Component } from '@angular/core';
import { HeaderComponent } from "./header/header.component";
import { InteractionsComponent } from './interactions/interactions.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  imports: [HeaderComponent, InteractionsComponent]
})
export class AppComponent {
  title = 'PeekLinkBot.Web';
}
