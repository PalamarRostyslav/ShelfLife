import { useState } from 'react';
import type { FormEvent } from 'react';
import type { LogSessionPayload } from '../api/types';

interface SessionFormProps {
  onSubmit: (payload: LogSessionPayload) => void;
  isSubmitting: boolean;
}

export function SessionForm({ onSubmit, isSubmitting }: SessionFormProps) {
  const [date, setDate] = useState(() => new Date().toISOString().slice(0, 10));
  const [pagesRead, setPagesRead] = useState('');
  const [minutesSpent, setMinutesSpent] = useState('');

  function handleSubmit(event: FormEvent) {
    event.preventDefault();
    onSubmit({
      date: new Date(date).toISOString(),
      pagesRead: Number(pagesRead),
      minutesSpent: Number(minutesSpent),
    });
    setPagesRead('');
    setMinutesSpent('');
  }

  return (
    <form onSubmit={handleSubmit} className="flex flex-wrap items-end gap-3">
      <label className="flex flex-col text-sm text-gray-600">
        Date
        <input
          type="date"
          value={date}
          onChange={(e) => setDate(e.target.value)}
          max={new Date().toISOString().slice(0, 10)}
          className="rounded border border-gray-300 px-2 py-1"
          required
        />
      </label>
      <label className="flex flex-col text-sm text-gray-600">
        Pages read
        <input
          type="number"
          min={1}
          value={pagesRead}
          onChange={(e) => setPagesRead(e.target.value)}
          className="w-24 rounded border border-gray-300 px-2 py-1"
          required
        />
      </label>
      <label className="flex flex-col text-sm text-gray-600">
        Minutes spent
        <input
          type="number"
          min={1}
          value={minutesSpent}
          onChange={(e) => setMinutesSpent(e.target.value)}
          className="w-24 rounded border border-gray-300 px-2 py-1"
          required
        />
      </label>
      <button
        type="submit"
        disabled={isSubmitting}
        className="rounded bg-gray-900 px-4 py-1.5 text-sm text-white disabled:opacity-50"
      >
        {isSubmitting ? 'Logging...' : 'Log session'}
      </button>
    </form>
  );
}
