import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { apiClient } from './client';
import type {
  AddBookPayload,
  BookDto,
  LogSessionPayload,
  MoveToShelfPayload,
  ReadingSessionDto,
  UpdateBookPayload,
} from './types';

function booksQueryKey(shelfId?: string) {
  return ['books', shelfId ?? 'all'] as const;
}

export function useBooks(shelfId?: string) {
  return useQuery({
    queryKey: booksQueryKey(shelfId),
    queryFn: () => {
      const params = shelfId ? `?shelfId=${shelfId}` : '';
      return apiClient.get<BookDto[]>(`/api/books${params}`);
    },
  });
}

export function useBook(id: string | undefined) {
  return useQuery({
    queryKey: ['books', 'detail', id],
    queryFn: () => apiClient.get<BookDto>(`/api/books/${id}`),
    enabled: Boolean(id),
  });
}

export function useCreateBook() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (payload: AddBookPayload) => apiClient.post<{ id: string }>('/api/books', payload),
    onSuccess: () => queryClient.invalidateQueries({ queryKey: ['books'] }),
  });
}

export function useUpdateBook(id: string) {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (payload: UpdateBookPayload) => apiClient.put<void>(`/api/books/${id}`, payload),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['books'] });
      queryClient.invalidateQueries({ queryKey: ['books', 'detail', id] });
    },
  });
}

export function useDeleteBook() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (id: string) => apiClient.delete<void>(`/api/books/${id}`),
    onSuccess: () => queryClient.invalidateQueries({ queryKey: ['books'] }),
  });
}

export function useMoveBookToShelf(id: string) {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (payload: MoveToShelfPayload) => apiClient.post<void>(`/api/books/${id}/shelf`, payload),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['books'] });
      queryClient.invalidateQueries({ queryKey: ['books', 'detail', id] });
    },
  });
}

export function useBookSessions(bookId: string | undefined) {
  return useQuery({
    queryKey: ['books', 'sessions', bookId],
    queryFn: () => apiClient.get<ReadingSessionDto[]>(`/api/books/${bookId}/sessions`),
    enabled: Boolean(bookId),
  });
}

export function useLogSession(bookId: string) {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (payload: LogSessionPayload) =>
      apiClient.post<{ id: string }>(`/api/books/${bookId}/sessions`, payload),
    onSuccess: () => queryClient.invalidateQueries({ queryKey: ['books', 'sessions', bookId] }),
  });
}
