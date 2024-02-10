import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Apollo, gql } from 'apollo-angular';
import { Employee } from 'src/app/models/employee';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css'],
})
export class EmployeeComponent implements OnInit {
  constructor(private apollo: Apollo, private router: Router) {}
  employees: Employee[] | null = null;

  ngOnInit() {
    this.getEmployees();
  }

  getEmployees() {
    this.apollo
      .watchQuery({
        query: gql`
          query {
            allEmployees {
              employeeNumber
              firstName
              lastName
            }
          }
        `,
      })
      .valueChanges.subscribe((result: any) => {
        this.employees = result.data.allEmployees;
      });
  }

  employeeProfile(employeeNumber: string) {
    this.router.navigate(['/employee/profile'], {
      queryParams: { query: employeeNumber },
    });
  }
}
