import { Component, OnDestroy, OnInit, inject } from '@angular/core';
import { HeroesService } from '../shared/services/heroes.service';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  heroesService = inject(HeroesService);
}
