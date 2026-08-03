import { useState } from "react";
import { Link } from "react-router-dom";
import { useBooks } from "../api/books";
import { useShelves } from "../api/shelves";
import { BookCard } from "../components/BookCard";
import { ShelfTabs } from "../components/ShelfTabs";

export function ShelfOverviewPage() {
  const [selectedShelfId, setSelectedShelfId] = useState<string | undefined>(
    undefined,
  );
  const shelvesQuery = useShelves();
  const booksQuery = useBooks(selectedShelfId);

  return (
    <div className="mx-auto max-w-4xl space-y-6 p-6">
      <div className="flex items-center justify-between">
        <h1 className="text-2xl font-semibold text-gray-900">Your shelves</h1>
        <Link
          to="/books/new"
          className="rounded bg-gray-900 px-4 py-1.5 text-sm text-white hover:bg-gray-800"
        >
          Add book
        </Link>
      </div>

      {shelvesQuery.data && (
        <ShelfTabs
          shelves={shelvesQuery.data}
          selectedShelfId={selectedShelfId}
          onSelect={setSelectedShelfId}
        />
      )}

      {booksQuery.isLoading && (
        <p className="text-sm text-gray-400">Loading books...</p>
      )}
      {booksQuery.data && booksQuery.data.length === 0 && (
        <p className="text-sm text-gray-400">No books on this shelf yet.</p>
      )}

      <div className="grid grid-cols-1 gap-4 sm:grid-cols-2 md:grid-cols-3">
        {booksQuery.data?.map((book) => (
          <BookCard key={book.id} book={book} />
        ))}
      </div>
    </div>
  );
}
