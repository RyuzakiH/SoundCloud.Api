﻿using SoundCloud.Api.Entities;
using SoundCloud.Api.Exceptions;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Web;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SoundCloud.Api.Endpoints
{
    internal sealed class Tracks : Endpoint, ITracks
    {
        private const string TrackArtworkDataKey = "track[artwork_data]";
        private const string TrackByIdPath = "tracks/{0}?";
        private const string TrackCommentsPath = "tracks/{0}/comments?";
        private const string TrackFavoritersPath = "tracks/{0}/favoriters?";
        private const string TrackPath = "tracks?";
        private const string TrackSecretTokenPath = "tracks/{0}/secret-token?";

        internal Tracks(ISoundCloudApiGateway gateway)
            : base(gateway)
        {
        }

        public IWebResult Delete(Track track)
        {
            EnsureToken();
            Validate(track.ValidateDelete);

            var builder = new TrackQueryBuilder();
            builder.Path = string.Format(TrackByIdPath, track.id);

            return Delete(builder.BuildUri());
        }

        public async Task<IWebResult> DeleteAsync(Track track)
        {
            EnsureToken();
            Validate(track.ValidateDelete);

            var builder = new TrackQueryBuilder();
            builder.Path = string.Format(TrackByIdPath, track.id);

            return await DeleteAsync(builder.BuildUri());
        }

        public Track Get(int trackId)
        {
            EnsureClientId();

            var builder = new TrackQueryBuilder();
            builder.Path = string.Format(TrackByIdPath, trackId);

            return GetById<Track>(builder.BuildUri());
        }

        public async Task<Track> GetAsync(int trackId)
        {
            EnsureClientId();

            var builder = new TrackQueryBuilder();
            builder.Path = string.Format(TrackByIdPath, trackId);

            return await GetByIdAsync<Track>(builder.BuildUri());
        }

        public IEnumerable<Track> Get()
        {
            EnsureClientId();

            return Get(new TrackQueryBuilder());
        }

        public async Task<IEnumerable<Track>> GetAsync()
        {
            EnsureClientId();

            return await GetAsync(new TrackQueryBuilder());
        }

        public IEnumerable<Track> Get(SoundCloudQueryBuilder builder)
        {
            EnsureClientId();

            builder.Path = TrackPath;
            builder.Paged = true;

            return GetList<Track>(builder.BuildUri());
        }
        
        public async Task<IEnumerable<Track>> GetAsync(SoundCloudQueryBuilder builder)
        {
            EnsureClientId();

            builder.Path = TrackPath;
            builder.Paged = true;

            return await GetListAsync<Track>(builder.BuildUri());
        }

        public IEnumerable<Comment> GetComments(Track track)
        {
            EnsureClientId();
            Validate(track.ValidateGet);

            var builder = new TrackQueryBuilder();
            builder.Path = string.Format(TrackCommentsPath, track.id);
            builder.Paged = true;

            return GetList<Comment>(builder.BuildUri());
        }

        public async Task<IEnumerable<Comment>> GetCommentsAsync(Track track)
        {
            EnsureClientId();
            Validate(track.ValidateGet);

            var builder = new TrackQueryBuilder();
            builder.Path = string.Format(TrackCommentsPath, track.id);
            builder.Paged = true;

            return await GetListAsync<Comment>(builder.BuildUri());
        }

        public IEnumerable<User> GetFavoriters(Track track)
        {
            EnsureClientId();
            Validate(track.ValidateGet);

            var builder = new TrackQueryBuilder();
            builder.Path = string.Format(TrackFavoritersPath, track.id);
            builder.Paged = true;

            return GetList<User>(builder.BuildUri());
        }

        public Task<IEnumerable<User>> GetFavoritersAsync(Track track)
        {
            throw new System.NotImplementedException();
        }

        public SecretToken GetSecretToken(Track track)
        {
            EnsureToken();
            Validate(track.ValidateGet);

            var builder = new TrackQueryBuilder();
            builder.Path = string.Format(TrackSecretTokenPath, track.id);

            return GetById<SecretToken>(builder.BuildUri());
        }

        public async Task<SecretToken> GetSecretTokenAsync(Track track)
        {
            EnsureToken();
            Validate(track.ValidateGet);

            var builder = new TrackQueryBuilder();
            builder.Path = string.Format(TrackSecretTokenPath, track.id);

            return await GetByIdAsync<SecretToken>(builder.BuildUri());
        }

        public IWebResult<Track> Update(Track track)
        {
            EnsureToken();
            Validate(track.ValidateUpdate);

            var builder = new TrackQueryBuilder();
            builder.Path = string.Format(TrackByIdPath, track.id);

            return Update<Track>(builder.BuildUri(), track);
        }

        public async Task<IWebResult<Track>> UpdateAsync(Track track)
        {
            EnsureToken();
            Validate(track.ValidateUpdate);

            var builder = new TrackQueryBuilder();
            builder.Path = string.Format(TrackByIdPath, track.id);

            return await UpdateAsync<Track>(builder.BuildUri(), track);
        }

        public IWebResult<Track> UploadArtwork(Track track, Stream file)
        {
            EnsureToken();
            Validate(track.ValidateUploadArtwork);

            var parameters = new Dictionary<string, object>();
            parameters.Add(TrackArtworkDataKey, file);

            var builder = new TrackQueryBuilder();
            builder.Path = string.Format(TrackByIdPath, track.id);

            return Update<Track>(builder.BuildUri(), parameters);
        }

        public async Task<IWebResult<Track>> UploadArtworkAsync(Track track, Stream file)
        {
            EnsureToken();
            Validate(track.ValidateUploadArtwork);

            var parameters = new Dictionary<string, object>();
            parameters.Add(TrackArtworkDataKey, file);

            var builder = new TrackQueryBuilder();
            builder.Path = string.Format(TrackByIdPath, track.id);

            return await UpdateAsync<Track>(builder.BuildUri(), parameters);
        }

        public IWebResult<Track> UploadTrack(string title, Stream file)
        {
            EnsureToken();

            if (string.IsNullOrEmpty(title))
            {
                throw new SoundCloudValidationException("Title must not be empty.");
            }

            var parameters = new Dictionary<string, object>();
            parameters.Add("oauth_token", Credentials.AccessToken);
            parameters.Add("track[title]", title);
            parameters.Add("track[asset_data]", file);

            var builder = new TrackQueryBuilder();
            builder.Path = TrackPath;

            return Create<Track>(builder.BuildUri(), parameters);
        }

        public async Task<IWebResult<Track>> UploadTrackAsync(string title, Stream file)
        {
            EnsureToken();

            if (string.IsNullOrEmpty(title))
            {
                throw new SoundCloudValidationException("Title must not be empty.");
            }

            var parameters = new Dictionary<string, object>();
            parameters.Add("oauth_token", Credentials.AccessToken);
            parameters.Add("track[title]", title);
            parameters.Add("track[asset_data]", file);

            var builder = new TrackQueryBuilder();
            builder.Path = TrackPath;

            return await CreateAsync<Track>(builder.BuildUri(), parameters);
        }
    }
}