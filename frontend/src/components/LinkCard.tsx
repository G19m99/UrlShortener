import xIcon from "../assets/xIcon.svg";

interface LinkCardProps {
  shortUrl: string;
  setShortUrl: (url: string) => void;
}

const LinkCard = ({ shortUrl, setShortUrl }: LinkCardProps) => {
  const handleClick = async () => {
    await navigator.clipboard.writeText(shortUrl);
  };

  return (
    <div className="flex flex-col justify-end mt-10 bg-white/5 px-3 text-white max-w-md mx-auto md:flex-row-reverse md:justify-between md:items-center">
      <button
        className="pr-1 pt-1 self-end md:pt-0 md:pr-0"
        onClick={() => setShortUrl("")}
      >
        <img src={xIcon} className="text-white" />
      </button>
      <div className="flex flex-col justify-center sm:flex-row gap-2">
        <span className="font-medium">Shortened URL: </span>
        <p className="italic cursor-pointer" onClick={handleClick}>
          {shortUrl}
        </p>
      </div>
    </div>
  );
};

export default LinkCard;
