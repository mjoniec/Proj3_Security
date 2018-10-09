import { Component } from '@angular/core';

@Component({
  selector: 'app-map-component',
  templateUrl: './map.component.html'
})
export class MapComponent {
  public map = 0;

  public incrementMap() {
    this.map++;
  }
}
