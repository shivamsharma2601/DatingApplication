import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nac',
  templateUrl: './nac.component.html',
  styleUrls: ['./nac.component.css']
})
export class NacComponent implements OnInit {
  model: any = {}
  // currentUser$: Observable<User>;

  constructor(public accountService: AccountService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
    // this.currentUser$ = this.accountService.currentUser$;
  }

  login() {
    this.accountService.login(this.model).subscribe(response => {
      this.router.navigateByUrl('/members');
    })
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }

  // getCurrentUser() {
  //   this.accountService.currentUser$.subscribe(user => {
  //     this.loggedIn = !!user; //turns object into boolean, if user is null its false otherwise its true
  //   }, error => {
  //     console.log(error);
  //   });
  // }
}

