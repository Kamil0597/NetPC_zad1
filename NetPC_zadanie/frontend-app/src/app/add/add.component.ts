import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ContactService, Contact } from '../services/contact.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add',
  imports: [CommonModule, FormsModule],
  templateUrl: './add.component.html',
  styleUrl: './add.component.scss'
})
export class AddComponent
{
  categories = ['Work', 'Private', 'Other'];
  subCategories = ['Boss', 'Client'];

  contact: Contact = {
    id: 0,
    name: '',
    surname: '',
    password: '',
    email: '',
    phone: '',
    birthDate: '',
    category: '',
    subCategory: ''
  };

  constructor(private contactService: ContactService, private router: Router) { }

  onSubmit() {
    this.contactService.createContact(this.contact).subscribe({
      next: () => {
        console.log('Kontakt dodany!');
        this.router.navigate(['/contacts']); 
      },
      error: (err) => console.error('Błąd podczas dodawania kontaktu:', err)
    });
  }
  goToContacts() {
    this.router.navigate(['/contacts']);
  }
}
