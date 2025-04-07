import { motion } from "framer-motion";
import { Link } from "lucide-react";
import { useState } from "react";
import ShortUrlDisplay from "./components/ShortUrlDisplay";
import ShortenUrlForm from "./components/ShortenUrlForm";

function App() {
  const [shortUrl, setShortUrl] = useState("");

  return (
    <div className="min-h-screen bg-gradient-to-br from-slate-50 to-slate-100 flex flex-col items-center justify-center p-4">
      <motion.div
        initial={{ opacity: 0, y: -20 }}
        animate={{ opacity: 1, y: 0 }}
        className="w-full max-w-md"
      >
        <header className="mb-8 text-center">
          <motion.div
            initial={{ scale: 0.8 }}
            animate={{ scale: 1 }}
            transition={{ type: "spring", stiffness: 300 }}
            className="inline-flex items-center justify-center mb-2"
          >
            <Link className="h-8 w-8 text-violet-600" />
          </motion.div>
          <h1 className="text-3xl font-bold text-slate-800">URL Shortener</h1>
          <p className="text-slate-500 mt-2">
            Shorten your long URLs with just one click
          </p>
        </header>

        <motion.div
          className="bg-white rounded-xl shadow-lg p-6 mb-6"
          initial={{ opacity: 0, y: 20 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ delay: 0.1 }}
        >
          <ShortenUrlForm setShortUrl={setShortUrl} />
        </motion.div>

        {shortUrl && <ShortUrlDisplay shortUrl={shortUrl} />}
      </motion.div>
    </div>
  );
}

export default App;
