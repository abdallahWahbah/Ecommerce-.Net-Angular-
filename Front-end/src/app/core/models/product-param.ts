export interface ProductParam {
    pageNumber?: number;
    pageSize?: number;
    search?: string;
    categoryId?: string;
    sortBy?: string;
    sortDirection?: 'asc' | 'desc';
  }