import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { EmployeeComponent } from './pages/employee/employee.component';
import { EmployeeProfileComponent } from './pages/employee-profile/employee-profile.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent }, // Default route
  { path: 'employee', component: EmployeeComponent },
  { path: 'employee/profile', component: EmployeeProfileComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
