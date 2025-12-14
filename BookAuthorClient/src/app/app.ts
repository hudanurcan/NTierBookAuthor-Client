import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { Authors } from './components/authors/authors';

@Component({
  selector: 'app-root',
  imports: [Authors],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
 
}
