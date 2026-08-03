import { useEffect, useState } from 'react';
import type { FormEvent } from 'react';
import { Link, useNavigate, useParams } from 'react-router-dom';
import { useBook, useCreateBook, useUpdateBook } from '../api/books';
import { useShelves } from '../api/shelves';
import type { BookFormat } from '../api/types';

const FORMATS: BookFormat[] = ['Physical', 'Ebook', 'Audiobook'];

export function BookFormPage() {
  const { id } = useParams<{ id?: string }>();
  const isEditing = Boolean(id);
  const navigate = useNavigate();

  const bookQuery = useBook(id);
  const shelvesQuery = useShelves();
  const createBook = useCreateBook();
  const updateBook = useUpdateBook(id ?? '');

  const [title, setTitle] = useState('');
  const [author, setAuthor] = useState('');
  const [isbn, setIsbn] = useState('');
  const [pageCount, setPageCount] = useState('');
  const [format, setFormat] = useState<BookFormat>('Physical');
  const [rating, setRating] = useState('');
  const [shelfId, setShelfId] = useState('');

  useEffect(() => {
    if (bookQuery.data) {
      setTitle(bookQuery.data.title);
      setAuthor(bookQuery.data.author);
      setIsbn(bookQuery.data.isbn ?? '');
      setPageCount(String(bookQuery.data.pageCount));
      setFormat(bookQuery.data.format);
      setRating(bookQuery.data.rating != null ? String(bookQuery.data.rating) : '');
    }
  }, [bookQuery.data]);

  useEffect(() => {
    if (!isEditing && !shelfId && shelvesQuery.data && shelvesQuery.data.length > 0) {
      setShelfId(shelvesQuery.data[0].id);
    }
  }, [isEditing, shelfId, shelvesQuery.data]);

  function handleSubmit(event: FormEvent) {
    event.preventDefault();

    if (isEditing && id) {
      updateBook.mutate(
        {
          title,
          author,
          isbn: isbn || null,
          pageCount: Number(pageCount),
          format,
          rating: rating ? Number(rating) : null,
        },
        { onSuccess: () => navigate(`/books/${id}`) },
      );
      return;
    }

    createBook.mutate(
      { title, author, isbn, pageCount: Number(pageCount), format, shelfId },
      { onSuccess: (result) => navigate(`/books/${result.id}`) },
    );
  }

  const isSubmitting = createBook.isPending || updateBook.isPending;

  return (
    <div className="mx-auto max-w-lg space-y-6 p-6">
      <Link to="/" className="text-sm text-gray-500 hover:underline">
        &larr; Back to shelves
      </Link>
      <h1 className="text-2xl font-semibold text-gray-900">{isEditing ? 'Edit book' : 'Add book'}</h1>

      <form onSubmit={handleSubmit} className="space-y-4">
        <label className="flex flex-col text-sm text-gray-600">
          Title
          <input
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            className="rounded border border-gray-300 px-3 py-2"
            required
          />
        </label>

        <label className="flex flex-col text-sm text-gray-600">
          Author
          <input
            value={author}
            onChange={(e) => setAuthor(e.target.value)}
            className="rounded border border-gray-300 px-3 py-2"
            required
          />
        </label>

        <label className="flex flex-col text-sm text-gray-600">
          ISBN
          <input
            value={isbn}
            onChange={(e) => setIsbn(e.target.value)}
            className="rounded border border-gray-300 px-3 py-2"
          />
        </label>

        <div className="flex gap-4">
          <label className="flex flex-1 flex-col text-sm text-gray-600">
            Page count
            <input
              type="number"
              min={1}
              value={pageCount}
              onChange={(e) => setPageCount(e.target.value)}
              className="rounded border border-gray-300 px-3 py-2"
              required
            />
          </label>

          <label className="flex flex-1 flex-col text-sm text-gray-600">
            Format
            <select
              value={format}
              onChange={(e) => setFormat(e.target.value as BookFormat)}
              className="rounded border border-gray-300 px-3 py-2"
            >
              {FORMATS.map((f) => (
                <option key={f} value={f}>
                  {f}
                </option>
              ))}
            </select>
          </label>
        </div>

        {isEditing ? (
          <label className="flex flex-col text-sm text-gray-600">
            Rating (1-5)
            <input
              type="number"
              min={1}
              max={5}
              value={rating}
              onChange={(e) => setRating(e.target.value)}
              className="rounded border border-gray-300 px-3 py-2"
            />
          </label>
        ) : (
          shelvesQuery.data && (
            <label className="flex flex-col text-sm text-gray-600">
              Shelf
              <select
                value={shelfId}
                onChange={(e) => setShelfId(e.target.value)}
                className="rounded border border-gray-300 px-3 py-2"
                required
              >
                {shelvesQuery.data.map((shelf) => (
                  <option key={shelf.id} value={shelf.id}>
                    {shelf.name}
                  </option>
                ))}
              </select>
            </label>
          )
        )}

        <button
          type="submit"
          disabled={isSubmitting}
          className="rounded bg-gray-900 px-4 py-2 text-sm text-white disabled:opacity-50"
        >
          {isSubmitting ? 'Saving...' : isEditing ? 'Save changes' : 'Add book'}
        </button>
      </form>
    </div>
  );
}
