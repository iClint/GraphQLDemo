import { Department } from "../enums/department";
import { Gender } from "../enums/gender";
import { Position } from "../enums/position";
import { Address } from "./address";

export interface Employee {
  __typename: string;
  employeeNumber: string;
  taxFileNumber: string;
  firstName: string;
  lastName: string;
  gender: Gender;
  dateOfBirth: Date;
  age: number,
  department: Department;
  position: Position;
  salary: number;
  hireDate: Date;
  sickLeaveHours: number;
  annualLeaveHours: number;
  usedSickLeaveHours: number;
  usedAnnualLeaveHours: number;
  residentialAddress: Address;
  postalAddress: Address;
  phoneNumbers: string[];
  emails: string[];
  isActive: boolean;
  skills: string[];
}
