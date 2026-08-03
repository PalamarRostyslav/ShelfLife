import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { apiClient } from './client';
import type { CreateShelfPayload, ShelfDto } from './types';

export function useShelves() {
  return useQuery({
    queryKey: ['shelves'],
    queryFn: () => apiClient.get<ShelfDto[]>('/api/shelves'),
  });
}

export function useCreateShelf() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (payload: CreateShelfPayload) => apiClient.post<{ id: string }>('/api/shelves', payload),
    onSuccess: () => queryClient.invalidateQueries({ queryKey: ['shelves'] }),
  });
}
