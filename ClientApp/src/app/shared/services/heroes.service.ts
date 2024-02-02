import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Subject, catchError, combineLatest, map, startWith, switchMap, tap } from 'rxjs';
import { AddHero, Hero } from '../models/hero.model';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class HeroesService {
  httpClient = inject(HttpClient);
  route = inject(Router);
  url = '/heroes';

  updateHeroStatus$ = new Subject<number>();
  createHero$ = new Subject<AddHero>();

  private heroCreated$ = this.createHero$.pipe(
    switchMap((hero) => this.httpClient.post<AddHero>(this.url, hero)),
    tap(() => this.route.navigate(['']))
  );

  private heroStatus$ = this.updateHeroStatus$.pipe(
    switchMap((hero) => this.httpClient.put<Hero>(`${this.url}/${hero}`, hero))
  );

  heroes$ = combineLatest([
    this.httpClient.get<Hero[]>(this.url).pipe(startWith(this.url)),
    this.heroStatus$.pipe(startWith(1)),
  ]).pipe(switchMap(() => this.httpClient.get<Hero[]>(this.url)));

  constructor() {
    this.heroes$.subscribe();
    this.heroStatus$.subscribe();
    this.heroCreated$.subscribe();
  }


}
