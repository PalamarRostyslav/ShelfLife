export type BookFormat = 'Physical' | 'Ebook' | 'Audiobook';

export interface BookDto {
  id: string;
  title: string;
  author: string;
  isbn: string | null;
  pageCount: number;
  format: BookFormat;
  rating: number | null;
  shelfId: string;
  addedAt: string;
  finishedAt: string | null;
}

export interface ShelfDto {
  id: string;
  name: string;
  isSystemShelf: boolean;
  systemShelfType: string | null;
}

export interface ReadingSessionDto {
  id: string;
  bookId: string;
  startTime: string;
  pagesRead: number;
  minutesSpent: number;
}

export interface AddBookPayload {
  title: string;
  author: string;
  isbn: string;
  pageCount: number;
  format: BookFormat;
  shelfId: string;
}

export interface UpdateBookPayload {
  title: string;
  author: string;
  isbn: string | null;
  pageCount: number;
  format: BookFormat;
  rating: number | null;
}

export interface MoveToShelfPayload {
  shelfId: string;
}

export interface LogSessionPayload {
  date: string;
  pagesRead: number;
  minutesSpent: number;
}

export interface CreateShelfPayload {
  name: string;
}
