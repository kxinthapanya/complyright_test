import { Component, inject } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { HeroesService } from '../shared/services/heroes.service';
import { Brands } from '../shared/models/brand.model';

@Component({
  selector: 'app-hero',
  templateUrl: './hero.component.html',
  styleUrls: ['./hero.component.css'],
})
export class HeroComponent {
  fb = inject(FormBuilder);
  heroesService = inject(HeroesService);

  isSubmitted: boolean = false;

  brands: Brands[] = [
    { id: 1, name: 'DC' },
    { id: 2, name: 'Marvel' },
  ];

  form = this.fb.group({
    name: ['', Validators.required],
    alias: ['', Validators.required],
    brands: ['', Validators.required],
  });

  trackByBrandId(index: any, item: Brands) {
    return item.id;
  }

  submit(values: any) {
    if (!this.form.valid) {
      this.isSubmitted = true;
      return false;
    } else {
      this.isSubmitted = false;
      const brand = this.brands.find(
        (x) => x.id == +this.form.controls.brands.value!
      );

      const hero = {
        name: this.form.controls.name.value,
        alias: this.form.controls.alias.value,
        brandId: brand?.id,
        brandName: brand?.name,
      };

      this.heroesService.createHero$.next(hero);
    }
    return true;
  }
}
