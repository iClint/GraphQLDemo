import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NavigationService {
  getPages(){
    return [
      {label: "Home", route: "/home"},
      {label: "Employees", route: "/employee"}
    ]
  }

constructor() { }

}
