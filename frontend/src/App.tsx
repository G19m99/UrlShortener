import "./App.css";
import Card from "./components/Card";
import LinkCard from "./components/LinkCard";
import useCreateShortUrl from "./hooks/useCreateShortUrl";

function App() {
  const { shortenRequest, shortUrl, setShortUrl, loading } =
    useCreateShortUrl();
  return (
    <main className="relative isolate overflow-hidden bg-gray-900 shadow-2xl w-screen h-full px-6 py-44 sm:px-36 xl:py-64">
      <h2 className="mx-auto max-w-2xl text-center text-3xl font-bold tracking-tight text-white sm:text-4xl">
        Shortify
      </h2>

      <p className="mx-auto mt-2 max-w-xl text-center text-lg leading-8 text-gray-300">
        Share Smarter, Not Harder! Turn Lengthy Links into Lightning-Fast Short
        URLs, Making Sharing a Breeze!
      </p>
      <Card getShortUrlReq={shortenRequest} />
      {!loading && shortUrl && (
        <LinkCard shortUrl={shortUrl} setShortUrl={setShortUrl} />
      )}
      <svg
        viewBox="0 0 1024 1024"
        className="absolute left-1/2 top-1/2 -z-10 h-[64rem] w-[64rem] -translate-x-1/2"
        aria-hidden="true"
      >
        <circle
          cx="512"
          cy="512"
          r="512"
          fill="url(#759c1415-0410-454c-8f7c-9a820de03641)"
          fillOpacity="0.7"
        ></circle>
        <defs>
          <radialGradient
            id="759c1415-0410-454c-8f7c-9a820de03641"
            cx="0"
            cy="0"
            r="1"
            gradientUnits="userSpaceOnUse"
            gradientTransform="translate(512 512) rotate(90) scale(512)"
          >
            <stop stopColor="#7775D6"></stop>
            <stop offset="1" stopColor="#7ED321" stopOpacity="0"></stop>
          </radialGradient>
        </defs>
      </svg>
    </main>
  );
}

export default App;
