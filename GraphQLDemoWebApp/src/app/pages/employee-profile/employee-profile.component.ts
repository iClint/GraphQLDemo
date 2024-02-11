import { DatePipe } from '@angular/common';
import { Component, ElementRef, HostListener, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Apollo, gql } from 'apollo-angular';
import { Employee } from 'src/app/models/employee';

@Component({
  selector: 'app-employee-profile',
  templateUrl: './employee-profile.component.html',
  styleUrls: ['./employee-profile.component.css'],
})
export class EmployeeProfileComponent implements OnInit {
  public employee?: Employee;
  public employeeNumber: string | null = null;

  constructor(private apollo: Apollo, private route: ActivatedRoute) {}

  ngOnInit() {
    this.route.queryParams.subscribe((params) => {
      this.employeeNumber = params['query'];
      this.getEmployeeProfile();
    });
  }

  getEmployeeProfile() {
    this.apollo
      .watchQuery({
        query: gql`
       query {
         employeeByEmployeeNumber(employeeNumber: "${this.employeeNumber}") {
           employeeNumber
           firstName
           lastName
           gender
           dateOfBirth
           age
           residentialAddress{
            line1
            line2
            city
            state
            postcode
           }
           postalAddress{
            line1
            line2
            city
            state
            postcode
           }
           skills
         }
       }
        `,
      })
      .valueChanges.subscribe((result: any) => {
        const tempEmployee = { ...result.data.employeeByEmployeeNumber };
        delete tempEmployee['__typename'];
        this.employee = tempEmployee as Employee;
      });
  }

  formatDate(dateString: Date): string | null {
    const date = new Date(dateString);
    return date
      ? new DatePipe('en-AU').transform(date, 'dd/MMM/yyyy', 'UTC')
      : null;
  }
}
