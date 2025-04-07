import { motion } from "framer-motion";
import { Check, Copy } from "lucide-react";
import { useState } from "react";

type ShortUrlDisplayProps = {
  shortUrl: string;
};

const ShortUrlDisplay = ({ shortUrl }: ShortUrlDisplayProps) => {
  const [copied, setCopied] = useState(false);
  const copyToClipboard = () => {
    navigator.clipboard.writeText(shortUrl);
    setCopied(true);
    setTimeout(() => setCopied(false), 2000);
  };
  return (
    <motion.div
      className="bg-white rounded-xl shadow-lg p-6 overflow-hidden"
      layout // Use layout for smooth height animations
      initial={{ opacity: 0 }}
      animate={{ opacity: 1 }}
      transition={{ type: "spring", stiffness: 300 }}
    >
      <h2 className="text-lg font-medium text-slate-800 mb-3">
        Your shortened URL
      </h2>
      <div className="flex items-center">
        <input
          type="text"
          value={shortUrl}
          readOnly
          className="flex-1 px-4 py-2 border border-slate-300 rounded-l-lg bg-slate-50 text-slate-800"
        />
        <motion.button
          onClick={copyToClipboard}
          className={`flex items-center justify-center px-4 py-2 rounded-r-lg ${
            copied ? "bg-green-500" : "bg-violet-600"
          } text-white`}
          whileHover={{ scale: 1.05 }}
          whileTap={{ scale: 0.95 }}
        >
          {copied ? <Check size={18} /> : <Copy size={18} />}
        </motion.button>
      </div>
      <p className="mt-2 text-sm text-slate-500">
        {copied ? "Copied to clipboard!" : "Click the button to copy"}
      </p>
    </motion.div>
  );
};

export default ShortUrlDisplay;
