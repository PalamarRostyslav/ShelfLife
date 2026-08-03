import type { ReadingSessionDto } from '../api/types';

interface SessionListProps {
  sessions: ReadingSessionDto[];
}

export function SessionList({ sessions }: SessionListProps) {
  if (sessions.length === 0) {
    return <p className="text-sm text-gray-400">No reading sessions logged yet.</p>;
  }

  return (
    <ul className="divide-y divide-gray-200">
      {sessions.map((session) => (
        <li key={session.id} className="flex items-center justify-between py-2 text-sm">
          <span className="text-gray-600">{new Date(session.startTime).toLocaleDateString()}</span>
          <span className="text-gray-900">{session.pagesRead} pages</span>
          <span className="text-gray-500">{session.minutesSpent} min</span>
        </li>
      ))}
    </ul>
  );
}
