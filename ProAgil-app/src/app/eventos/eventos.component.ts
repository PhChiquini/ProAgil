import { Component, OnInit, TemplateRef } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { EventoService } from '../Services/Evento.service';
import { Evento } from '../Models/Evento';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { defineLocale, BsLocaleService, ptBrLocale } from 'ngx-bootstrap';
import { templateJitUrl } from '@angular/compiler';
import { ToastrService } from 'ngx-toastr';

defineLocale('pt-br', ptBrLocale);

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  titulo = 'Eventos';

  eventosFiltrados: Evento[];
  eventos: Evento[];
  evento: Evento;
  imagemLargura = 50;
  imagemMargem = 2;
  mostrarImagem = false;
  registerForm: FormGroup;
  modoSalvar = '';
  bodyDeletarEvento = '';

  file: File;
  fileNameToUpdate: string;
  dataAtual: string;

  // tslint:disable-next-line: variable-name
  _filtroLista = null;

  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService,
    private fb: FormBuilder,
    private localeSerevice: BsLocaleService,
    private toastr: ToastrService
  ) {
    this.localeSerevice.use('pt-br');
  }

  openModal(template: any) {
    this.registerForm.reset();
    template.show();
  }

  ngOnInit() {
    this.validate();
    this.getEventos();
  }

  filtrarEventos(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  alternarImagem() {
    this.mostrarImagem = !this.mostrarImagem;
  }

  validate() {
    this.registerForm = this.fb.group({
      tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      qtdPessoas: ['', [Validators.required, Validators.max(120000)]],
      imagemURL: ['', Validators.required],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]]
    });
  }

  novoEvento(template: any) {
    this.modoSalvar = 'post';
    this.openModal(template);
  }

  editarEvento(evento: Evento, template: any) {
    this.modoSalvar = 'put';
    this.evento = Object.assign({}, evento);
    this.fileNameToUpdate = evento.imagemURL.toString();

    this.evento.imagemURL = '';

    this.openModal(template);
    this.registerForm.patchValue(this.evento);
    console.log('registerform: ' + this.registerForm.value);
  }

  excluirEvento(evento: Evento, template: any) {
    this.openModal(template);
    this.evento = evento;
    this.bodyDeletarEvento = `Tem certeza que deseja excluir o Evento: "${evento.tema}"?`;
  }

  confirmDelete(template: any) {
    this.eventoService.deleteEvento(this.evento.id).subscribe(
      () => {
        template.hide();
        this.getEventos();
        this.toastr.success('Deletado com Sucesso.');
      },
      error => {
        this.toastr.error('Houve algum Problema.\nDescrição do erro:' + error);
      }
    );
  }

  onFileChange(event) {
    const reader = new FileReader();

    if (event.target.files && event.target.files.length) {

      this.file = event.target.files;

    }
  }

  uploadImagem() {

    const fakeDirectory = this.evento.imagemURL.split('\\', 3);
    this.evento.imagemURL = fakeDirectory[2];

    if (this.modoSalvar === 'post') {
      this.eventoService.postUpload(this.file, fakeDirectory[2])
        .subscribe(
          () => {
            this.dataAtual = new Date().getMilliseconds().toString();
          }
        );

    } else {
      this.evento.imagemURL = this.fileNameToUpdate;
      this.eventoService.postUpload(this.file, this.fileNameToUpdate)
        .subscribe(
          () => {
            this.dataAtual = new Date().getMilliseconds().toString();
          }
        );

    }
  }

  salvarAlteracao(template: any) {

    if (this.registerForm.valid) {
      if (this.modoSalvar === 'post') {

        this.evento = Object.assign({}, this.registerForm.value);

        this.uploadImagem();

        this.eventoService.postEvento(this.evento).subscribe(
          (novoEvento: Evento) => {
            template.hide();
            this.getEventos();
            this.toastr.success('Inserido com Sucesso.');
          }, error => {
            this.toastr.error('Houve algum Problema.\nDescrição do erro:' + error);
            console.log(error);
          }
        );
      } else {
        this.evento = Object.assign({ id: this.evento.id }, this.registerForm.value);

        this.uploadImagem();

        this.eventoService.putEvento(this.evento.id, this.evento).subscribe(
          (novoEvento: Evento) => {
            template.hide();
            this.getEventos();
            this.toastr.success('Editado com Sucesso.');
          }, error => {
            this.toastr.error('Houve algum Problema.\nDescrição do erro:' + error);
            console.log(error);
          }
        );
      }
    }
  }

  getEventos() {
    this.eventoService.getAllEvento().subscribe(
      // tslint:disable-next-line: variable-name
      (_eventos: Evento[]) => {
        this.eventos = _eventos;
        this.eventosFiltrados = this.eventos;
      },
      error => {
        this.toastr.error('Houve algum Problema.\nDescrição do erro:' + error);
      }
    );
  }

  get filtroLista(): string {
    return this._filtroLista;
  }
  set filtroLista(value: string) {
    this._filtroLista = value;
    this.eventosFiltrados = this._filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }

}
