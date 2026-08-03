import { Link } from 'react-router-dom';
import type { BookDto } from '../api/types';

interface BookCardProps {
  book: BookDto;
}

export function BookCard({ book }: BookCardProps) {
  return (
    <Link
      to={`/books/${book.id}`}
      className="block rounded-lg border border-gray-200 p-4 shadow-sm transition hover:border-gray-300 hover:shadow-md"
    >
      <h3 className="font-semibold text-gray-900">{book.title}</h3>
      <p className="text-sm text-gray-500">{book.author}</p>
      <div className="mt-2 flex items-center gap-2 text-xs text-gray-400">
        <span>{book.format}</span>
        <span>&middot;</span>
        <span>{book.pageCount} pages</span>
        {book.rating != null && (
          <>
            <span>&middot;</span>
            <span>{book.rating}/5</span>
          </>
        )}
      </div>
    </Link>
  );
}
