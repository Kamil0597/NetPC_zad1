import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { ContactService, Contact } from '../services/contact.service';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-contact-list',
  imports: [CommonModule],
  templateUrl: './contact-list.component.html'
})
export class ContactListComponent implements OnInit {
  contacts: Contact[] = [];
  isLoggedIn = false;

  constructor(private router: Router, private contactService: ContactService, public authService: AuthService) { }

  ngOnInit() {
    this.isLoggedIn = this.authService.isLoggedIn();

    this.contactService.getAllContacts().subscribe({
      next: (data) => this.contacts = data,
      error: (err) => console.error('Błąd pobierania kontaktów:', err)
    });
  }

  goToLogin() { this.router.navigate(['/login']); }
  goToRegister() { this.router.navigate(['/register']); }
  goToHome() { this.router.navigate(['/']); }
  addContact() { this.router.navigate(['/add']); }
  editContact(id: number) { this.router.navigate(['/edit', id]); }
  logout() { this.authService.logout(); this.router.navigate(['/']); }
}
