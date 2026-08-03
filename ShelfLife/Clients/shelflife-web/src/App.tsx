import { Route, Routes } from "react-router-dom";
import { BookDetailPage } from "./pages/BookDetailPage";
import { BookFormPage } from "./pages/BookFormPage";
import { ShelfOverviewPage } from "./pages/ShelfOverviewPage";

export default function App() {
  return (
    <Routes>
      <Route path="/" element={<ShelfOverviewPage />} />
      <Route path="/books/new" element={<BookFormPage />} />
      <Route path="/books/:id" element={<BookDetailPage />} />
      <Route path="/books/:id/edit" element={<BookFormPage />} />
    </Routes>
  );
}
