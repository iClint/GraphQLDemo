import { Component, OnInit } from '@angular/core';
import { NavigationService } from '../../Services/navigation.service';
import { NavPage } from 'src/app/models/navPage';
import { Router, ActivatedRoute, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css'],
})
export class NavigationComponent implements OnInit {
  navPages: NavPage[];
  activeRoute: string = '';

  constructor(
    private navigationService: NavigationService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.navPages = [];
    this.router.events
      .pipe(filter((event) => event instanceof NavigationEnd))
      .subscribe(() => {
        this.activeRoute =
          this.route.snapshot.firstChild?.routeConfig?.path || '';
      });
  }

  ngOnInit() {
    this.navPages = this.navigationService.getPages();
  }
}
