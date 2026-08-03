import type { ShelfDto } from '../api/types';

interface ShelfTabsProps {
  shelves: ShelfDto[];
  selectedShelfId: string | undefined;
  onSelect: (shelfId: string | undefined) => void;
}

export function ShelfTabs({ shelves, selectedShelfId, onSelect }: ShelfTabsProps) {
  return (
    <div className="flex flex-wrap gap-2 border-b border-gray-200 pb-2">
      <button
        type="button"
        onClick={() => onSelect(undefined)}
        className={`rounded-full px-3 py-1 text-sm ${
          selectedShelfId === undefined
            ? 'bg-gray-900 text-white'
            : 'bg-gray-100 text-gray-600 hover:bg-gray-200'
        }`}
      >
        All
      </button>
      {shelves.map((shelf) => (
        <button
          key={shelf.id}
          type="button"
          onClick={() => onSelect(shelf.id)}
          className={`rounded-full px-3 py-1 text-sm ${
            selectedShelfId === shelf.id
              ? 'bg-gray-900 text-white'
              : 'bg-gray-100 text-gray-600 hover:bg-gray-200'
          }`}
        >
          {shelf.name}
        </button>
      ))}
    </div>
  );
}
