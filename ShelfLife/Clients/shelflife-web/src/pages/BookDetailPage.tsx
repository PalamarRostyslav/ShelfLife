import { Link, useNavigate, useParams } from 'react-router-dom';
import { useBook, useBookSessions, useDeleteBook, useLogSession, useMoveBookToShelf } from '../api/books';
import { useShelves } from '../api/shelves';
import { SessionForm } from '../components/SessionForm';
import { SessionList } from '../components/SessionList';

export function BookDetailPage() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();

  const bookQuery = useBook(id);
  const sessionsQuery = useBookSessions(id);
  const shelvesQuery = useShelves();
  const logSession = useLogSession(id!);
  const moveToShelf = useMoveBookToShelf(id!);
  const deleteBook = useDeleteBook();

  if (bookQuery.isLoading) {
    return <p className="p-6 text-sm text-gray-400">Loading book...</p>;
  }

  if (!bookQuery.data) {
    return <p className="p-6 text-sm text-gray-400">Book not found.</p>;
  }

  const book = bookQuery.data;

  function handleDelete() {
    if (!id) return;
    if (!confirm('Delete this book? This cannot be undone.')) return;
    deleteBook.mutate(id, { onSuccess: () => navigate('/') });
  }

  return (
    <div className="mx-auto max-w-3xl space-y-8 p-6">
      <Link to="/" className="text-sm text-gray-500 hover:underline">
        &larr; Back to shelves
      </Link>

      <div className="flex items-start justify-between">
        <div>
          <h1 className="text-2xl font-semibold text-gray-900">{book.title}</h1>
          <p className="text-gray-500">{book.author}</p>
        </div>
        <div className="flex gap-2">
          <Link
            to={`/books/${book.id}/edit`}
            className="rounded border border-gray-300 px-3 py-1.5 text-sm hover:bg-gray-50"
          >
            Edit
          </Link>
          <button
            type="button"
            onClick={handleDelete}
            className="rounded border border-red-200 px-3 py-1.5 text-sm text-red-600 hover:bg-red-50"
          >
            Delete
          </button>
        </div>
      </div>

      <dl className="grid grid-cols-2 gap-x-4 gap-y-2 text-sm sm:grid-cols-4">
        <div>
          <dt className="text-gray-400">Format</dt>
          <dd className="text-gray-900">{book.format}</dd>
        </div>
        <div>
          <dt className="text-gray-400">Pages</dt>
          <dd className="text-gray-900">{book.pageCount}</dd>
        </div>
        <div>
          <dt className="text-gray-400">Rating</dt>
          <dd className="text-gray-900">{book.rating ?? '—'}</dd>
        </div>
        <div>
          <dt className="text-gray-400">ISBN</dt>
          <dd className="text-gray-900">{book.isbn ?? '—'}</dd>
        </div>
      </dl>

      {shelvesQuery.data && (
        <div className="flex items-center gap-2 text-sm">
          <label htmlFor="shelf-select" className="text-gray-500">
            Shelf
          </label>
          <select
            id="shelf-select"
            value={book.shelfId}
            onChange={(e) => moveToShelf.mutate({ shelfId: e.target.value })}
            className="rounded border border-gray-300 px-2 py-1"
          >
            {shelvesQuery.data.map((shelf) => (
              <option key={shelf.id} value={shelf.id}>
                {shelf.name}
              </option>
            ))}
          </select>
        </div>
      )}

      <section className="space-y-3">
        <h2 className="text-lg font-medium text-gray-900">Reading sessions</h2>
        <SessionForm
          isSubmitting={logSession.isPending}
          onSubmit={(payload) => logSession.mutate(payload)}
        />
        {sessionsQuery.data && <SessionList sessions={sessionsQuery.data} />}
      </section>
    </div>
  );
}
